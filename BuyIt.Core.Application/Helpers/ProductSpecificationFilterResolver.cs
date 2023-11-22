using System.Reflection;
using Application.Contracts;
using Application.DataTransferObjects.ProductRelated.Specification;
using Application.FilteringModels;
using Application.Specifications.ProductManufacturerSpecifications;
using Application.Specifications.ProductSpecifications;
using Application.Specifications.ProductTypeSpecifications;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.Helpers;

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
        IRepository<ProductSpecification> productSpecs,
        IRepository<ProductManufacturer> manufacturers,
        IRepository<ProductType> categories,
        IFilteringModel filteringModel)
    {
        var productManufacturers = await manufacturers.GetAllEntitiesAsync(
            new ProductManufacturerQuerySpecification());

        var specifications = filteringModel.GetType() != typeof(ProductSearchFilteringModel) 
            ? await productSpecs.GetAllEntitiesAsync(
            new ProductSpecificationQuerySpecification(
                s => s.Products.Any(
                    p => p.ProductType.Name.Equals(filteringModel.Category.First())))) 
            : new List<ProductSpecification>();
        
        var filteredProducts = await products.GetAllEntitiesAsync(
            GetQuerySpecification(filteringModel));

        var categoryRelatedProducts = await GetCategoryRelatedProducts(
            products, filteringModel, filteredProducts);
        
        var commonSpecifications = GetCommonSpecifications(
            categoryRelatedProducts, filteredProducts, specifications, productManufacturers, filteringModel);
        
        var countedBrands = await GetBrandCountsAsync(
            products, manufacturers, filteringModel);
        
        var countedSpecs = GetCountedSpecifications(
            filteringModel, filteredProducts, categoryRelatedProducts,
            specifications, commonSpecifications);
        
        var countedCategories = await GetCategoriesCountsAsync(
            categories, filteredProducts, filteringModel);

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
        return filteringModel is ProductSearchFilteringModel 
               || filteredProducts.Count == 0 
            ? new List<Product>() 
            : (await products.GetAllEntitiesAsync(new ProductQuerySpecification(
                product => product.ProductType.Name.Equals(
                    filteredProducts.First().ProductType.Name)))).Except(filteredProducts);
    }
    
    private IEnumerable<ProductSpecification> GetCommonSpecifications(
        IEnumerable<Product> productsOfType, 
        IEnumerable<Product> filteredProducts,
        IEnumerable<ProductSpecification> specifications,
        IEnumerable<ProductManufacturer> manufacturers,
        IFilteringModel filteringModel)
    {
        var commonSpecifications = new List<ProductSpecification>();
        var productSpecs = GetAllSpecifications(specifications, productsOfType);
        var filteredSpecs = GetAllSpecifications(specifications, filteredProducts);

        ExtractSelectedFilters(filteringModel, productSpecs, manufacturers, commonSpecifications);
        
        ExtractSelectionRelatedFilters(
            productsOfType, filteredProducts, filteredSpecs, productSpecs, commonSpecifications);

        return commonSpecifications.DistinctBy(specification => specification.SpecificationValue.Value);
    }

    private static void ExtractSelectionRelatedFilters(IEnumerable<Product> productsOfType,
        IEnumerable<Product> filteredProducts,
        IEnumerable<ProductSpecification> filteredSpecs, 
        IEnumerable<ProductSpecification> productSpecs, 
        List<ProductSpecification> commonSpecifications)
    {
        foreach (var filteredProduct in filteredProducts)
        {
            foreach (var product in productsOfType)
            {
                if (filteredSpecs.Any(filteredSpec =>
                        productSpecs.All(productSpec =>
                            productSpec.SpecificationValue.Value.Equals(filteredSpec.SpecificationValue.Value)) &&
                        filteredProduct.Manufacturer.Name.Equals(product.Manufacturer.Name)))
                    commonSpecifications.AddRange(productSpecs);
            }
        }
    }

    private void ExtractSelectedFilters(IFilteringModel filteringModel, 
        IEnumerable<ProductSpecification> productSpecs,
        IEnumerable<ProductManufacturer> manufacturers,
        List<ProductSpecification> commonSpecifications)
    {
        foreach (var propertyName in filteringModel.MappedFilterNamings.Keys)
        {
            var propertyList = (List<string>)filteringModel.GetType().GetProperty(propertyName)?.GetValue(filteringModel);

            if (propertyList == null || !propertyList.Any()) continue;

            var specValues = filteringModel.MappedFilterNamings[propertyName];

            CheckSpecificationValidity(productSpecs, commonSpecifications, specValues);
        }

        ClearBrandsUnrelatedFilters(filteringModel, manufacturers, commonSpecifications);
    }

    private static void CheckSpecificationValidity(IEnumerable<ProductSpecification> productSpecs,
        List<ProductSpecification> commonSpecifications,
        IDictionary<string, string> specValues)
    {
        foreach (var specCategoryValue in specValues)
        {
            var specCategory = specCategoryValue.Key;
            var specAttribute = specCategoryValue.Value;

            if (productSpecs.Any(spec =>
                    spec.SpecificationCategory.Value.Equals(specCategory) &&
                    spec.SpecificationAttribute.Value.Equals(specAttribute)))
            {
                commonSpecifications.AddRange(productSpecs.Where(
                    specification => specification.SpecificationCategory.Value.Equals(specCategory) &&
                                     specification.SpecificationAttribute.Value.Equals(specAttribute)));
            }
        }
    }

    private void ClearBrandsUnrelatedFilters(IFilteringModel filteringModel,
        IEnumerable<ProductManufacturer> manufacturers,
        List<ProductSpecification> commonSpecifications)
    {
        var brands = (List<string>)filteringModel.GetType().GetProperty(
            "BrandName")?.GetValue(filteringModel);

        if (!brands!.Any()) return;

        var relativeSpecs = (from specification in commonSpecifications 
            let productsBrands = specification.Products.Select(
                product => manufacturers.Single(m =>
                    m.Id == product.ManufacturerId).Name).ToList() 
            where brands.Any(brand => productsBrands.Any(brand.Equals))
            select specification).ToList();

        commonSpecifications.Clear();
        commonSpecifications.AddRange(relativeSpecs);
    }

    private IEnumerable<ProductSpecification> GetAllSpecifications(
        IEnumerable<ProductSpecification> productSpecifications,
        IEnumerable<Product> products)
    {
        return productSpecifications
            .Where(spec =>
                !_removedSpecsCategories.Contains(spec.SpecificationCategory.Value) &&
                !_removedSpecsAttributes.Contains(spec.SpecificationAttribute.Value) &&
                products.Any(product => spec.Products.Contains(product)))
            .ToList();
    }

    private IQuerySpecification<Product> GetQuerySpecification(IFilteringModel filteringModel)
    {
        var result = filteringModel.CreateQuerySpecification();
        
        result.IsPagingEnabled = false;

        return result;
    }

    private IDictionary<string, int> GetCountedSpecifications(IFilteringModel filteringModel,
        IEnumerable<Product> filteredProducts,
        IEnumerable<Product> productsOfType,
        IEnumerable<ProductSpecification> allSpecs,
        IEnumerable<ProductSpecification> relatedSpecifications)
    {
        var countedSpecs = new Dictionary<string, int>();
    
        if (filteringModel is ProductSearchFilteringModel)
            return countedSpecs;
    
        var allSpecifications = relatedSpecifications.Any() 
            ? GetAllSpecifications(allSpecs, filteredProducts).Union(relatedSpecifications) 
            : GetAllSpecifications(allSpecs, filteredProducts);
        
        CountSpecifications(allSpecifications, countedSpecs, filteredProducts, productsOfType);
    
        return countedSpecs;
    }

    private void CountSpecifications(IEnumerable<ProductSpecification> allSpecifications,
        IDictionary<string, int> countedSpecs, 
        IEnumerable<Product> filteredProducts,
        IEnumerable<Product> productsOfType)
    {
        foreach (var specification in allSpecifications)
        {
            var spec = new SpecificationDto
            {
                Category = specification.SpecificationCategory.Value,
                Attribute = specification.SpecificationAttribute.Value,
                Value = specification.SpecificationValue.Value
            };

            var count = filteredProducts.Any(product => specification.Products.Contains(product)) 
                ? filteredProducts.Count(product => specification.Products.Contains(product))
                : productsOfType.Count(product => specification.Products.Contains(product));

            if (!countedSpecs.Where(_ => countedSpecs.ContainsKey(spec.ToString()!)).IsNullOrEmpty())
                continue;
    
            countedSpecs.Add(spec.ToString()!, count);
        }
    }

    private async Task<IDictionary<string, int>> GetBrandCountsAsync(
        IRepository<Product> productRepository,
        IRepository<ProductManufacturer> brandsRepository,
        IFilteringModel filteringModel)
    {
        var filterListsDictionary = GetFilterListsDictionary(filteringModel, 
            filteringModel.GetType().GetProperties().Where(
                info => info.PropertyType == typeof(List<string>)));

        if ((IsSatisfyingProductModelPredicate(filteringModel, filterListsDictionary) ||
            IsSatisfyingSearchModelPredicate(filteringModel, filterListsDictionary))
            && IsWithoutPriceLimits(filteringModel))
            return RemoveZeroCountsFromDictionary(
                await GetAllCountedCategoryRelatedManufacturersAsync(
                    brandsRepository, productRepository, filteringModel));

        return RemoveZeroCountsFromDictionary(await GetCountedBrandsToDictionaryAsync(productRepository,
            filteringModel));
    }

    private async Task<IDictionary<string, int>> GetAllCountedCategoryRelatedManufacturersAsync(
        IRepository<ProductManufacturer> brandsRepository, IRepository<Product> productRepository,
        IFilteringModel filteringModel)
    {
        var productManufacturers = await brandsRepository.GetAllEntitiesAsync(
            new ProductManufacturerQuerySpecification());

        if (filteringModel.GetType() != typeof(ProductSearchFilteringModel))
        {
            var categoryRelatedProducts = await productRepository.GetAllEntitiesAsync(
                new ProductQuerySpecification(product => product.ProductType.Name.Equals(
                    filteringModel.Category.First())));
            
            return productManufacturers.ToDictionary(
                brand => brand.Name,
                brand => categoryRelatedProducts
                    .Count(product => product.Manufacturer.Name.Equals(brand.Name)));
        }

        var searchedItems = await productRepository.GetAllEntitiesAsync(
                new ProductSearchQuerySpecification((ProductSearchFilteringModel)filteringModel));
        
        return productManufacturers.ToDictionary(
            brand => brand.Name,
            brand => searchedItems
                .Count(product => product.Manufacturer.Name.Equals(brand.Name)));
    }
    
    private async Task<IDictionary<string, int>> GetCategoriesCountsAsync(
        IRepository<ProductType> categoriesRepository, IEnumerable<Product> filteredProducts,
        IFilteringModel filteringModel)
    {
        if (filteringModel.GetType() != typeof(ProductSearchFilteringModel))
            return new Dictionary<string, int>();
        
        return await GetCountedCategoriesToDictionaryAsync(categoriesRepository, filteredProducts,
                new ProductTypeQueryByManufacturerSpecification(
                    filteredProducts.Select(product => product.Manufacturer.Name)));
    }

    private async Task<IDictionary<string, int>> GetCountedBrandsToDictionaryAsync(
        IRepository<Product> productRepository,
        IFilteringModel filteringModel)
    {
        filteringModel.BrandName = new List<string>();

        var selectedFilterProducts = await productRepository.GetAllEntitiesAsync(
            GetQuerySpecification(filteringModel));

        var result = new Dictionary<string, int>();

        foreach (var product in selectedFilterProducts.Where(
                     product => !result.ContainsKey(product.Manufacturer.Name)))
            result.Add(product.Manufacturer.Name, selectedFilterProducts.Count(selectedProduct =>
                selectedProduct.Manufacturer.Name == product.Manufacturer.Name));

        return result;
    }

    private async Task<IDictionary<string, int>> GetCountedCategoriesToDictionaryAsync(
        IRepository<ProductType> categoriesRepository, 
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