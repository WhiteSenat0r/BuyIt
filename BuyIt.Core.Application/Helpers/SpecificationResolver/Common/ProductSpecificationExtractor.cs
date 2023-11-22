using Application.Contracts;
using Application.Helpers.SpecificationResolver.Common.Constants;
using Domain.Entities;

namespace Application.Helpers.SpecificationResolver.Common;

public sealed class ProductSpecificationExtractor
{
    private readonly IEnumerable<Product> _categoryRelatedProducts;
    private readonly IEnumerable<Product> _filteredProducts;
    private readonly IEnumerable<ProductSpecification> _allSpecifications;
    private readonly IEnumerable<ProductManufacturer> _manufacturers;
    private readonly IFilteringModel _filteringModel;
    private readonly FilterCategoryConstants _filterCategoryConstants;
    private readonly FilterAttributeConstants _filterAttributeConstants;

    public ProductSpecificationExtractor(IEnumerable<Product> categoryRelatedProducts,
        IEnumerable<Product> filteredProducts, 
        IEnumerable<ProductSpecification> allSpecifications, 
        IEnumerable<ProductManufacturer> manufacturers, 
        IFilteringModel filteringModel, 
        FilterCategoryConstants filterCategoryConstants, 
        FilterAttributeConstants filterAttributeConstants)
    {
        _categoryRelatedProducts = categoryRelatedProducts;
        _filteredProducts = filteredProducts;
        _allSpecifications = allSpecifications;
        _manufacturers = manufacturers;
        _filteringModel = filteringModel;
        _filterCategoryConstants = filterCategoryConstants;
        _filterAttributeConstants = filterAttributeConstants;
    }
    
    public IEnumerable<ProductSpecification> ExtractCommonSpecifications()
    {
        var commonSpecifications = new List<ProductSpecification>();
        
        var productSpecs = GetAllSpecifications(
            _allSpecifications, _categoryRelatedProducts);
        var filteredSpecs = GetAllSpecifications(
            _allSpecifications, _filteredProducts);

        ExtractSelectedFilters(_filteringModel, productSpecs, _manufacturers, commonSpecifications);
        
        ExtractSelectionRelatedFilters(_categoryRelatedProducts, _filteredProducts,
            filteredSpecs, productSpecs, commonSpecifications);

        return commonSpecifications.DistinctBy(specification => specification.SpecificationValue.Value);
    }

    public IEnumerable<ProductSpecification> ExtractSpecificationsForCounting(
        IEnumerable<ProductSpecification> commonSpecifications) => commonSpecifications.Any() 
            ? GetAllSpecifications(_allSpecifications, _filteredProducts).Union(commonSpecifications) 
            : GetAllSpecifications(_allSpecifications, _filteredProducts);

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
                            productSpec.SpecificationValue.Value.Equals(
                                filteredSpec.SpecificationValue.Value)) &&
                        filteredProduct.Manufacturer.Name.Equals(product.Manufacturer.Name)))
                    commonSpecifications.AddRange(productSpecs);
            }
        }
    }

    private void ExtractSelectedFilters(IFilteringModel filteringModel, 
        IEnumerable<ProductSpecification> productSpecs,
        IEnumerable<ProductManufacturer> manufacturers,
        IEnumerable<ProductSpecification> commonSpecifications)
    {
        foreach (var propertyName in filteringModel.MappedFilterNamings.Keys)
        {
            var propertyList = (List<string>)filteringModel.GetType()
                .GetProperty(propertyName)?.GetValue(filteringModel);

            if (propertyList == null || !propertyList.Any()) continue;

            var specValues = filteringModel.MappedFilterNamings[propertyName];

            CheckSpecificationValidity(productSpecs, commonSpecifications, specValues);
        }

        ClearBrandsUnrelatedFilters(filteringModel, manufacturers, commonSpecifications);
    }
    
    private static void CheckSpecificationValidity(IEnumerable<ProductSpecification> productSpecs,
        IEnumerable<ProductSpecification> commonSpecifications,
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
                (commonSpecifications as List<ProductSpecification>)!.AddRange(
                    productSpecs.Where(specification => 
                        specification.SpecificationCategory.Value.Equals(specCategory) &&
                        specification.SpecificationAttribute.Value.Equals(specAttribute)));
            }
        }
    }

    private void ClearBrandsUnrelatedFilters(IFilteringModel filteringModel,
        IEnumerable<ProductManufacturer> manufacturers,
        IEnumerable<ProductSpecification> commonSpecifications)
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

        (commonSpecifications as List<ProductSpecification>)!.Clear();
        (commonSpecifications as List<ProductSpecification>)!.AddRange(relativeSpecs);
    }
    
    private IEnumerable<ProductSpecification> GetAllSpecifications(
        IEnumerable<ProductSpecification> productSpecifications,
        IEnumerable<Product> products)
    {
        return productSpecifications
            .Where(spec =>
                !_filterCategoryConstants.RemovedCategories.Contains(
                    spec.SpecificationCategory.Value) &&
                !_filterAttributeConstants.RemovedAttributes.Contains(
                    spec.SpecificationAttribute.Value) &&
                products.Any(product => spec.Products.Contains(product)))
            .ToList();
    }
}