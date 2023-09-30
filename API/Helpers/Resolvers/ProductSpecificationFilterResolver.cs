using System.Reflection;
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
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductTypeQueries;
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
        IRepository<ProductManufacturer> manufacturers,
        IRepository<ProductType> categories,
        IFilteringModel filteringModel)
    {
        var filteredProducts = await products.GetAllEntitiesAsync(
            GetQuerySpecification(filteringModel));
        
        var countedBrands = await GetBrandCounts(
            (ProductManufacturerRepository)manufacturers, filteredProducts, filteringModel);

        var countedSpecs = GetCountedSpecifications(filteringModel, filteredProducts);
        
        var countedCategories = await GetCategoriesCounts(
            (ProductTypeRepository)categories, filteredProducts, filteringModel);

        return new FilterDto
        {
            CountedBrands = countedBrands,
            CountedSpecifications = countedSpecs,
            CountedCategories = countedCategories,
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
    
    private IQuerySpecification<Product> GetQuerySpecification(IFilteringModel filteringModel) =>
        filteringModel.GetType().Name switch
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

    private IDictionary<string, int> GetCountedSpecifications(IFilteringModel filteringModel,
        IEnumerable<Product> filteredProducts)
    {
        var countedSpecs = new Dictionary<string, int>();

        if (filteringModel.GetType().Name.Equals("ProductSearchFilteringModel"))
            return countedSpecs;

        var allSpecifications = GetAllSpecifications(filteredProducts);
        
        CountSpecifications(allSpecifications, countedSpecs);

        return countedSpecs;
    }

    private void CountSpecifications(
        IEnumerable<ProductSpecification> allSpecifications, IDictionary<string, int> countedSpecs)
    {
        foreach (var specification in allSpecifications)
        {
            var spec = new SpecificationDto
            {
                Category = specification.SpecificationCategory.Value,
                Attribute = specification.SpecificationAttribute.Value,
                Value = specification.SpecificationValue.Value
            };

            var count = allSpecifications.Count(GetSpecificationCountPredicate(spec));

            if (!countedSpecs.Where(_ => countedSpecs.ContainsKey(spec.ToString()!)).IsNullOrEmpty())
                continue;

            countedSpecs.Add(spec.ToString()!, count);
        }
    }

    private Func<ProductSpecification, bool> GetSpecificationCountPredicate(
        SpecificationDto spec) =>
        productSpecification =>
            productSpecification.SpecificationCategory.Value.Equals(spec.Category) &&
            productSpecification.SpecificationAttribute.Value.Equals(spec.Attribute) &&
            productSpecification.SpecificationValue.Value.Equals(spec.Value);

    private async Task<IDictionary<string, int>> GetBrandCounts(
        ProductManufacturerRepository brandsRepository, IEnumerable<Product> filteredProducts,
        IFilteringModel filteringModel)
    {
        var filterListsDictionary = GetFilterListsDictionary(filteringModel, 
            filteringModel.GetType().GetProperties().Where(
                info => info.PropertyType == typeof(List<string>)));

        if ((IsSatisfyingProductModelPredicate(filteringModel, filterListsDictionary) ||
            IsSatisfyingSearchModelPredicate(filteringModel, filterListsDictionary))
            && IsWithoutPriceLimits(filteringModel))
            return await brandsRepository.GetAllCountedCategoryRelatedManufacturers(filteringModel);

        return !filteringModel.Category.IsNullOrEmpty() 
            ? await GetCountedBrandsToDictionary(brandsRepository, filteredProducts,
                new ProductManufacturerByProductTypeQuerySpecification(filteringModel.Category.First()))
            : await GetCountedBrandsToDictionary(brandsRepository, filteredProducts,
                new ProductManufacturerQuerySpecification());
    }
    
    private async Task<IDictionary<string, int>> GetCategoriesCounts(
        ProductTypeRepository categoriesRepository, IEnumerable<Product> filteredProducts,
        IFilteringModel filteringModel)
    {
        if (!filteringModel.GetType().Name.Equals("ProductSearchFilteringModel"))
            return new Dictionary<string, int>();
        
        return await GetCountedCategoriesToDictionary(categoriesRepository, filteredProducts,
                new ProductTypeQueryByManufacturerSpecification(
                    filteredProducts.Select(product => product.Manufacturer.Name)));
    }

    private async Task<IDictionary<string, int>> GetCountedBrandsToDictionary(
        ProductManufacturerRepository brandsRepository, 
        IEnumerable<Product> filteredProducts, IQuerySpecification<ProductManufacturer> querySpecification) =>
        (await brandsRepository.GetAllEntitiesAsync(querySpecification))
        .ToDictionary(
            brand => brand.Name,
            brand =>
            {
                return filteredProducts.Count(
                    product => product.Manufacturer.Name == brand.Name);
            });
    
    private async Task<IDictionary<string, int>> GetCountedCategoriesToDictionary(
        ProductTypeRepository categoriesRepository, 
        IEnumerable<Product> filteredProducts, IQuerySpecification<ProductType> querySpecification) =>
        (await categoriesRepository.GetAllEntitiesAsync(querySpecification))
        .ToDictionary(
            category => category.Name,
            category =>
            {
                return filteredProducts.Count(
                    product => product.ProductType.Name == category.Name);
            });

    private Dictionary<string, List<string>> GetFilterListsDictionary(
        IFilteringModel filteringModel, IEnumerable<PropertyInfo> filterProperties) =>
        filterProperties.ToDictionary(
            key => key.Name,
            value => (List<string>)value.GetValue(filteringModel));

    private bool IsSatisfyingSearchModelPredicate(IFilteringModel filteringModel, 
        IReadOnlyDictionary<string, List<string>> filterListsDictionary) =>
        filteringModel is ProductSearchFilteringModel 
        && filterListsDictionary["BrandName"].IsNullOrEmpty()
        && filterListsDictionary["Category"].IsNullOrEmpty();

    private bool IsWithoutPriceLimits(IFilteringModel filteringModel) =>
        filteringModel.UpperPriceLimit is null
        && filteringModel.LowerPriceLimit is null;

    private bool IsSatisfyingProductModelPredicate(IFilteringModel filteringModel, 
        IReadOnlyDictionary<string, List<string>> filterListsDictionary) =>
        !filterListsDictionary["BrandName"].IsNullOrEmpty()
        && filterListsDictionary.Where(pair => !pair.Key.Equals("BrandName") 
                                               && !pair.Key.Equals(
                                                   "Category")).All(pair => pair.Value.IsNullOrEmpty())
        && filteringModel is not ProductSearchFilteringModel;
}