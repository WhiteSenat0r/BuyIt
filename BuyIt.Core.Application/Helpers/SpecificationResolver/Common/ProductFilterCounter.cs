using System.Reflection;
using Application.Contracts;
using Application.DataTransferObjects.ProductRelated.Specification;
using Application.FilteringModels;
using Domain.Entities.ProductRelated;
using Microsoft.IdentityModel.Tokens;

namespace Application.Helpers.SpecificationResolver.Common;

public sealed class ProductFilterCounter
{
    private readonly IEnumerable<Product> _categoryRelatedProducts;
    private readonly IEnumerable<Product> _filteredProducts;
    private readonly IEnumerable<Product> _brandlessFilteredProducts;
    private readonly IEnumerable<ProductSpecification> _commonSpecifications;
    private readonly IEnumerable<ProductManufacturer> _manufacturers;
    private readonly IEnumerable<ProductType> _categories;
    private readonly IFilteringModel _filteringModel;
    private readonly ProductSpecificationExtractor _specificationExtractor;

    public ProductFilterCounter(IEnumerable<Product> categoryRelatedProducts,
        IEnumerable<Product> filteredProducts,
        IEnumerable<Product> brandlessFilteredProducts,
        IEnumerable<ProductSpecification> commonSpecifications,
        IEnumerable<ProductManufacturer> manufacturers,
        IEnumerable<ProductType> categories,
        IFilteringModel filteringModel,
        ProductSpecificationExtractor specificationExtractor)
    {
        _categoryRelatedProducts = categoryRelatedProducts;
        _filteredProducts = filteredProducts;
        _manufacturers = manufacturers;
        _categories = categories;
        _filteringModel = filteringModel;
        _specificationExtractor = specificationExtractor;
        _brandlessFilteredProducts = brandlessFilteredProducts;
        _commonSpecifications = commonSpecifications;
    }
    
    public IDictionary<string, int> GetCountedSpecifications()
    {
        var countedSpecs = new Dictionary<string, int>();
    
        if (_filteringModel is ProductSearchFilteringModel)
            return countedSpecs;
    
        var extractedSpecification = _specificationExtractor.ExtractSpecificationsForCounting(
            _commonSpecifications);
        
        CountSpecifications(
            extractedSpecification, countedSpecs, _filteredProducts, _categoryRelatedProducts);
    
        return countedSpecs;
    }
    
    public IDictionary<string, int> GetCountedBrands()
    {
        var filterListsDictionary = GetFilterListsDictionary(_filteringModel, 
            _filteringModel.GetType().GetProperties().Where(
                info => info.PropertyType == typeof(List<string>)));

        if ((IsSatisfyingProductModelPredicate(_filteringModel, filterListsDictionary) ||
             IsSatisfyingSearchModelPredicate(_filteringModel, filterListsDictionary))
            && IsWithoutPriceLimits(_filteringModel))
            return RemoveZeroCountsFromDictionary(
                GetAllCountedCategoryRelatedManufacturers());

        return RemoveZeroCountsFromDictionary(GetCountedBrandsToDictionary());
    }
    
    public IDictionary<string, int> GetCountedCategories() =>
        _filteringModel.GetType() != typeof(ProductSearchFilteringModel) 
            ? new Dictionary<string, int>() 
            : RemoveZeroCountsFromDictionary(GetCountedCategoriesToDictionary());

    private IDictionary<string, int> GetCountedCategoriesToDictionary()
    {
        var extractedFilteredProductBrands = _filteringModel.BrandName;
        var extractedProductBrands = _filteredProducts.Select(product => product.Manufacturer);

        if (IsPresentSearchText())
            return !extractedFilteredProductBrands.IsNullOrEmpty()
                ? GetCategoryByBrandsDictionary(extractedFilteredProductBrands)
                : GetCategoryByExtractedBrandsDictionary(extractedProductBrands);
        
        return !AreBrandFiltersSelected()
            ? GetCategoryByBrandsDictionary(extractedFilteredProductBrands)
            : GetCountedCategoriesDictionary();
    }

    private Dictionary<string, int> GetCountedCategoriesDictionary() =>
        _categories
            .ToDictionary(
                category => category.Name,
                category => category.Products.Count);

    private bool AreBrandFiltersSelected() => _filteringModel.BrandName.IsNullOrEmpty();

    private bool IsPresentSearchText() => 
        !((ProductSearchFilteringModel)_filteringModel).Text.IsNullOrEmpty();

    private Dictionary<string, int> GetCategoryByExtractedBrandsDictionary(
        IEnumerable<ProductManufacturer> extractedProductBrands) =>
        _categories
            .ToDictionary(
                category => category.Name,
                category => category.Products.Count(
                    product => extractedProductBrands.Contains(product.Manufacturer)));

    private Dictionary<string, int> GetCategoryByBrandsDictionary(
        ICollection<string> extractedFilteredProductBrands) =>
        _categories
            .ToDictionary(
                category => category.Name,
                category => category.Products.Count(
                    product => extractedFilteredProductBrands.Contains(
                        product.Manufacturer.Name)));

    private IDictionary<string, int> GetAllCountedCategoryRelatedManufacturers()
    {
        if (_filteringModel.GetType() != typeof(ProductSearchFilteringModel))
        {
            return _manufacturers.ToDictionary(
                brand => brand.Name,
                brand => _categoryRelatedProducts.Union(_filteredProducts)
                    .Count(product => product.Manufacturer.Name.Equals(brand.Name)));
        }
        
        return _manufacturers.ToDictionary(
            brand => brand.Name,
            brand => _filteredProducts
                .Count(product => product.Manufacturer.Name.Equals(brand.Name)));
    }
    
    private IDictionary<string, int> GetCountedBrandsToDictionary()
    {
        var result = new Dictionary<string, int>();

        foreach (var product in _brandlessFilteredProducts.Where(
                     product => !result.ContainsKey(product.Manufacturer.Name)))
            result.Add(product.Manufacturer.Name, _brandlessFilteredProducts.Count(selectedProduct =>
                selectedProduct.Manufacturer.Name == product.Manufacturer.Name));

        return result;
    }
    
    private IDictionary<string, List<string>> GetFilterListsDictionary(
        IFilteringModel filteringModel, IEnumerable<PropertyInfo> filterProperties) =>
        filterProperties.ToDictionary(
            key => key.Name,
            value => (List<string>)value.GetValue(filteringModel));
    
    private bool IsWithoutPriceLimits(IFilteringModel filteringModel) =>
        filteringModel.UpperPriceLimit is null
        && filteringModel.LowerPriceLimit is null;

    private bool IsSatisfyingProductModelPredicate(IFilteringModel filteringModel, 
        IDictionary<string, List<string>> filterListsDictionary) =>
        !filterListsDictionary["BrandName"].IsNullOrEmpty()
        && filterListsDictionary.Where(pair => !pair.Key.Equals("BrandName") 
                                               && !pair.Key.Equals(
                                                   "Category")).All(pair => pair.Value.IsNullOrEmpty())
        && filteringModel is not ProductSearchFilteringModel;
    
    private IDictionary<string, int> RemoveZeroCountsFromDictionary(
        IDictionary<string, int> countedElements) =>
        countedElements.Where(
                kv => kv.Value != 0).
            ToDictionary(
                kv => kv.Key,
                kv => kv.Value);

    private bool IsSatisfyingSearchModelPredicate(IFilteringModel filteringModel, 
        IDictionary<string, List<string>> filterListsDictionary) =>
        filteringModel is ProductSearchFilteringModel 
        && filterListsDictionary["BrandName"].IsNullOrEmpty()
        && filterListsDictionary["Category"].IsNullOrEmpty();
    
    private void CountSpecifications(IEnumerable<ProductSpecification> allSpecifications,
        IDictionary<string, int> countedSpecs, 
        IEnumerable<Product> filteredProducts,
        IEnumerable<Product> categoryRelatedProducts)
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
                : categoryRelatedProducts.Count(product => specification.Products.Contains(product));

            if (!countedSpecs.Where(_ => countedSpecs.ContainsKey(spec.ToString()!)).IsNullOrEmpty())
                continue;
    
            countedSpecs.Add(spec.ToString()!, count);
        }
    }
}