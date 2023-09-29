using API.Helpers.DataTransferObjects.ProductRelated.Specification;
using Core.Entities.Product;
using Core.Entities.Product.ProductSpecificationRelated;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.ComputerRelated;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelatedSpecifications;
using Microsoft.IdentityModel.Tokens;

namespace API.Helpers.Resolvers;

public sealed class ProductSpecificationFilterResolver
{
    private readonly string[] _removedSpecsCategories =
    {
        "Measurements", "Interfaces and connection"
    };
    
    private readonly string[] _removedSpecsAttributes = 
    {
        "Base clock", "Max clock", "Processor technology", 
        "Quantity of threads", "Type of memory", "Memory bus"
    };

    public async Task<FilterDto> Resolve(IRepository<Product> products,
        IRepository<ProductManufacturer> manufacturers, IFilteringModel filteringModel)
    {
        var filteredProducts = await products.GetAllEntitiesAsync(
            GetQuerySpecification(filteringModel));
        
        var brandCounts = await GetBrandCounts(
            (ProductManufacturerRepository)manufacturers, filteredProducts, filteringModel);
        
        var allSpecifications = GetAllSpecifications(filteredProducts);

        var countedSpecs = GetCountedSpecifications(allSpecifications);

        return new FilterDto
        {
            CountedBrands = brandCounts,
            CountedSpecifications = countedSpecs,
            TotalCount = filteredProducts.Count,
            MinPrice = (int)filteredProducts.MinBy(
                product => Math.Round(product.Price)).Price,
            MaxPrice = Convert.ToInt32(filteredProducts.MaxBy(product => Math.Round(product.Price)).Price),
        };
    }

    private IEnumerable<ProductSpecification> GetAllSpecifications(IEnumerable<Product> filteredProducts)
    {
        return filteredProducts
            .SelectMany(p => p.Specifications)
            .Where(spec =>
                !_removedSpecsCategories.Contains(spec.SpecificationCategory.Value) &&
                !_removedSpecsAttributes.Contains(spec.SpecificationAttribute.Value))
            .ToList();
    }
    
    private IQuerySpecification<Product> GetQuerySpecification(IFilteringModel filteringModel)
    {
        return filteringModel.GetType().Name switch
        {
            "PersonalComputerFilteringModel" => new PersonalComputerQuerySpecification(
                (PersonalComputerFilteringModel)filteringModel),
            "LaptopFilteringModel" => new LaptopQuerySpecification(
                (LaptopFilteringModel)filteringModel),
            "AioComputerFilteringModel" => new AioComputerQuerySpecification(
                (AioComputerFilteringModel)filteringModel),
            "ProductSearchFilteringModel" => new ProductSearchQuerySpecification(
                (ProductSearchFilteringModel)filteringModel),
            _ => throw new ArgumentException("Unknown filtering model was passed!")
        };
    }

    private IDictionary<string, int> GetCountedSpecifications(IEnumerable
        <ProductSpecification> allSpecifications)
    {
        var countedSpecs = new Dictionary<string, int>();

        foreach (var specification in allSpecifications)
        {
            var spec = new SpecificationDto
            {
                Category = specification.SpecificationCategory.Value,
                Attribute = specification.SpecificationAttribute.Value,
                Value = specification.SpecificationValue.Value
            };

            var count = allSpecifications.Count(productSpecification =>
                productSpecification.SpecificationCategory.Value.Equals(spec.Category) &&
                productSpecification.SpecificationAttribute.Value.Equals(spec.Attribute) &&
                productSpecification.SpecificationValue.Value.Equals(spec.Value));

            if (!countedSpecs.Where(_ => countedSpecs.ContainsKey(spec.ToString()!)).IsNullOrEmpty())
                continue;

            countedSpecs.Add(spec.ToString()!, count);
        }

        return countedSpecs;
    }

    private async Task<IDictionary<string, int>> GetBrandCounts(
        ProductManufacturerRepository brandsRepository, IEnumerable<Product> filteredProducts,
        IFilteringModel filteringModel)
    {
        var filterProperties = filteringModel.GetType().GetProperties()
            .Where(info => info.PropertyType == typeof(List<string>));

        var filterListsDictionary = filterProperties.ToDictionary(
            key => key.Name,
            value => (List<string>)value.GetValue(filteringModel));

        if (!filterListsDictionary["BrandName"].IsNullOrEmpty()
            && filterListsDictionary.Where(pair => !pair.Key.Equals("BrandName") 
                                                   && !pair.Key.Equals(
                                                       "Category")).All(pair => pair.Value.IsNullOrEmpty())
            && filteringModel.GetType() != typeof(ProductSearchFilteringModel)
            && filteringModel.UpperPriceLimit is null 
            && filteringModel.LowerPriceLimit is null)
            return await brandsRepository.GetAllCountedCategoryRelatedManufacturers(filteringModel);

        return (await brandsRepository.GetAllEntitiesAsync(
                new ProductManufacturerByProductTypeQuerySpecification(
                    filteringModel.Category.First())))
            .ToDictionary(
                brand => brand.Name,
                brand =>
                {
                    return filteredProducts.Count(
                        product => product.Manufacturer.Name == brand.Name);
                });
    }
}