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
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications;
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
        "Quantity of threads", "Type of memory", "Memory bus",
        "Drive's interface"
    };

    public async Task<FilterDto> ResolveAsync(IRepository<Product> products,
        IRepository<ProductManufacturer> manufacturers,
        IRepository<ProductType> categories,
        IFilteringModel filteringModel)
    {
        var filteredProducts = await products.GetAllEntitiesAsync(
            GetQuerySpecification(filteringModel));

        var categoryRelatedProducts = await GetCategoryRelatedProducts(
            products, filteringModel, filteredProducts);
        
        var countedBrands = await GetBrandCountsAsync(
            (ProductManufacturerRepository)manufacturers, filteredProducts, filteringModel);
        
        var commonSpecifications = GetCommonSpecifications(
            categoryRelatedProducts, filteredProducts);
        
        var countedSpecs = GetCountedSpecifications(
            filteringModel, filteredProducts, commonSpecifications);
        
        var countedCategories = await GetCategoriesCountsAsync(
            (ProductTypeRepository)categories, filteredProducts, filteringModel);

        return new FilterDto
        {
            CountedBrands = countedBrands.OrderBy(pair => pair.Key).ToDictionary(
                pair => pair.Key, pair => pair.Value),
            CountedSpecifications = countedSpecs,
            CountedCategories = countedCategories,
            MinPrice = filteredProducts.Count > 0 ? (int)filteredProducts.MinBy(
                product => Math.Round(product.Price)).Price : 0,
            MaxPrice = filteredProducts.Count > 0 ? Convert.ToInt32(filteredProducts.MaxBy(
                product => Math.Round(product.Price)).Price) : 0
        };
    }

    private static async Task<IEnumerable<Product>> GetCategoryRelatedProducts(
        IRepository<Product> products, IFilteringModel filteringModel,
        IReadOnlyCollection<Product> filteredProducts)
    {
        return filteringModel.GetType().Name.Equals(
            "ProductSearchFilteringModel") || filteredProducts.Count == 0 
            ? new List<Product>() 
            : (await products.GetAllEntitiesAsync(new ProductQuerySpecification(
                product => product.ProductType.Name.Equals(
                    filteredProducts.First().ProductType.Name)))).Except(filteredProducts);
    }

    private IEnumerable<ProductSpecification> GetCommonSpecifications(
        IEnumerable<Product> productsOfType, IEnumerable<Product> filteredProducts)
    {
        var commonSpecifications = new List<ProductSpecification>();
        
        foreach (var filteredProduct in filteredProducts)
        {
            var filteredSpecs = GetAllSpecifications(
                new[] { filteredProduct });
    
            foreach (var product in productsOfType)
            {
                var productSpecs = GetAllSpecifications(
                    new[] { product });

                if (filteredSpecs.Any(filteredSpec =>
                        productSpecs.All(specification => specification.SpecificationValue
                            .Value.Equals(filteredSpec.SpecificationValue.Value)) &&
                        filteredProduct.Manufacturer.Name.Equals(product.Manufacturer.Name)))
                    commonSpecifications.AddRange(productSpecs);
            }
        }
    
        return commonSpecifications.Distinct();
    }
    
    private IEnumerable<ProductSpecification> GetAllSpecifications(
        IEnumerable<Product> filteredProducts)
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
        IEnumerable<Product> filteredProducts, IEnumerable<ProductSpecification> relatedSpecifications)
    {
        var countedSpecs = new Dictionary<string, int>();
    
        if (filteringModel.GetType().Name.Equals("ProductSearchFilteringModel"))
            return countedSpecs;
    
        var allSpecifications = relatedSpecifications.Any() 
            ? GetAllSpecifications(filteredProducts).Union(relatedSpecifications) 
            : GetAllSpecifications(filteredProducts);
        
        CountSpecifications(allSpecifications, countedSpecs);
    
        return countedSpecs;
    }

    private void CountSpecifications(IEnumerable<ProductSpecification> allSpecifications,
        IDictionary<string, int> countedSpecs)
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

    private async Task<IDictionary<string, int>> GetBrandCountsAsync(
        ProductManufacturerRepository brandsRepository, IEnumerable<Product> filteredProducts,
        IFilteringModel filteringModel)
    {
        var filterListsDictionary = GetFilterListsDictionary(filteringModel, 
            filteringModel.GetType().GetProperties().Where(
                info => info.PropertyType == typeof(List<string>)));

        if ((IsSatisfyingProductModelPredicate(filteringModel, filterListsDictionary) ||
            IsSatisfyingSearchModelPredicate(filteringModel, filterListsDictionary))
            && IsWithoutPriceLimits(filteringModel))
            return RemoveZeroCountsFromDictionary(
                await brandsRepository.GetAllCountedCategoryRelatedManufacturersAsync(filteringModel));

        return !filteringModel.Category.IsNullOrEmpty() 
            ? RemoveZeroCountsFromDictionary(await GetCountedBrandsToDictionaryAsync(
                brandsRepository, filteredProducts, 
                new ProductManufacturerByProductTypeQuerySpecification(filteringModel.Category.First())))
            : RemoveZeroCountsFromDictionary(await GetCountedBrandsToDictionaryAsync(
                brandsRepository, filteredProducts,
                new ProductManufacturerQuerySpecification()));
    }
    
    private async Task<IDictionary<string, int>> GetCategoriesCountsAsync(
        ProductTypeRepository categoriesRepository, IEnumerable<Product> filteredProducts,
        IFilteringModel filteringModel)
    {
        if (!filteringModel.GetType().Name.Equals("ProductSearchFilteringModel"))
            return new Dictionary<string, int>();
        
        return await GetCountedCategoriesToDictionaryAsync(categoriesRepository, filteredProducts,
                new ProductTypeQueryByManufacturerSpecification(
                    filteredProducts.Select(product => product.Manufacturer.Name)));
    }

    private async Task<IDictionary<string, int>> GetCountedBrandsToDictionaryAsync(
        ProductManufacturerRepository brandsRepository, 
        IEnumerable<Product> filteredProducts, 
        IQuerySpecification<ProductManufacturer> querySpecification) =>
        (await brandsRepository.GetAllEntitiesAsync(querySpecification))
        .ToDictionary(
            brand => brand.Name,
            brand =>
            {
                return filteredProducts.Count(
                    product => product.Manufacturer.Name == brand.Name);
            });
    
    private async Task<IDictionary<string, int>> GetCountedCategoriesToDictionaryAsync(
        ProductTypeRepository categoriesRepository, 
        IEnumerable<Product> filteredProducts, 
        IQuerySpecification<ProductType> querySpecification) =>
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
    
    private IDictionary<string, int> RemoveZeroCountsFromDictionary(
        IDictionary<string, int> countedElements) =>
        countedElements.Where(
                kv => kv.Value != 0).
            ToDictionary(
                kv => kv.Key,
                kv => kv.Value);

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