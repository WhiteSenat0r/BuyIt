using Application.Contracts;
using Application.FilteringModels;
using Application.Helpers.SpecificationResolver.Common;
using Application.Helpers.SpecificationResolver.Common.Constants;
using Domain.Entities;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.Helpers.Resolvers;

public class ProductSpecificationExtractorTests
{
    private IEnumerable<Product> _categoryRelatedProducts;
    private IEnumerable<Product> _filteredProducts;
    private IEnumerable<ProductSpecification> _allSpecifications;
    private IEnumerable<ProductManufacturer> _manufacturers;
    private IFilteringModel _filteringModel;
    private FilterCategoryConstants _filterCategoryConstants;
    private FilterAttributeConstants _filterAttributeConstants;
    
    [Fact]
    public void Constructor_InitializesFieldsCorrectly()
    {
        _categoryRelatedProducts = new List<Product> { new() };
        _filteredProducts = new List<Product> { new() };
        _allSpecifications = new List<ProductSpecification> { new() };
        _manufacturers = new List<ProductManufacturer> { new() };
        _filteringModel = new ProductSearchFilteringModel();
        _filterCategoryConstants = new FilterCategoryConstants();
        _filterAttributeConstants = new FilterAttributeConstants();

        var extractor = new ProductSpecificationExtractor(
            _categoryRelatedProducts, _filteredProducts,
            _allSpecifications, _manufacturers, _filteringModel,
            _filterCategoryConstants, _filterAttributeConstants);
        
        Assert.NotNull(extractor);
    }
    
    [Fact]
    public void ExtractCommonSpecifications_Method_GetsRelevantSpecificationsCorrectly()
    {
        _manufacturers = new List<ProductManufacturer> { new("TestM") };

        var productSpecCategory = new ProductSpecificationCategory("General");
        var productSpecAttribute = new ProductSpecificationAttribute("Operating system");
        var productSpecValue = new ProductSpecificationValue("Operating system");
        
        var deletedSpecCategory = new ProductSpecificationCategory("Processor");
        var deletedSpecAttribute = new ProductSpecificationAttribute("Processor technology");
        var deletedSpecValue = new ProductSpecificationValue("Test");
        
        _allSpecifications = new List<ProductSpecification>
        {
            new(productSpecCategory.Id, productSpecAttribute.Id, productSpecValue.Id)
            {
                SpecificationCategory = productSpecCategory,
                SpecificationAttribute = productSpecAttribute,
                SpecificationValue = productSpecValue
            },
            new(deletedSpecCategory.Id, deletedSpecAttribute.Id, deletedSpecValue.Id)
            {
                SpecificationCategory = deletedSpecCategory,
                SpecificationAttribute = deletedSpecAttribute,
                SpecificationValue = deletedSpecValue
            },
        };
        
        _categoryRelatedProducts = new List<Product>
        {
            new ("Test", "Test", 1m,
                true, _manufacturers.First(),
                new ProductType("Personal computer"),
                new ProductRating(), new [] { "1.jpg", "2.jpg" } ),
            new ("Test", "Test", 1m,
                true, _manufacturers.First(),
                new ProductType("Personal computer"),
                new ProductRating(), new [] { "1.jpg", "2.jpg" } )
        };
        
        _categoryRelatedProducts.First().Specifications.Add(_allSpecifications.First());
        _categoryRelatedProducts.First().Specifications.Add(_allSpecifications.Last());
        _categoryRelatedProducts.Last().Specifications.Add(_allSpecifications.First());
        
        _manufacturers.First().Products.Add(_categoryRelatedProducts.First());
        
        _filteredProducts = _categoryRelatedProducts;
        
        _filteringModel = new ProductSearchFilteringModel();
        _filterCategoryConstants = new FilterCategoryConstants();
        _filterAttributeConstants = new FilterAttributeConstants();

        var extractor = new ProductSpecificationExtractor(
            _categoryRelatedProducts, _filteredProducts,
            _allSpecifications, _manufacturers, _filteringModel,
            _filterCategoryConstants, _filterAttributeConstants);

        var extractedSpecs = extractor.ExtractCommonSpecifications();
        
        Assert.Empty(extractedSpecs);
    }
    
    [Fact]
    public void ExtractSpecificationsForCounting_Method_GetsRelevantSpecificationsCorrectly()
    {
        _manufacturers = new List<ProductManufacturer> { new("TestM") };

        var productSpecCategory = new ProductSpecificationCategory("General");
        var productSpecAttribute = new ProductSpecificationAttribute("Operating system");
        var productSpecValue = new ProductSpecificationValue("Operating system");
        
        var deletedSpecCategory = new ProductSpecificationCategory("Processor");
        var deletedSpecAttribute = new ProductSpecificationAttribute("Processor technology");
        var deletedSpecValue = new ProductSpecificationValue("Test");
        
        _allSpecifications = new List<ProductSpecification>
        {
            new(productSpecCategory.Id, productSpecAttribute.Id, productSpecValue.Id)
            {
                SpecificationCategory = productSpecCategory,
                SpecificationAttribute = productSpecAttribute,
                SpecificationValue = productSpecValue
            },
            new(deletedSpecCategory.Id, deletedSpecAttribute.Id, deletedSpecValue.Id)
            {
                SpecificationCategory = deletedSpecCategory,
                SpecificationAttribute = deletedSpecAttribute,
                SpecificationValue = deletedSpecValue
            },
        };
        
        _categoryRelatedProducts = new List<Product>
        {
            new ("Test", "Test", 1m,
                true, _manufacturers.First(),
                new ProductType("Personal computer"),
                new ProductRating(), new [] { "1.jpg", "2.jpg" } )
        };
        
        _categoryRelatedProducts.First().Specifications.Add(_allSpecifications.First());
        
        _manufacturers.First().Products.Add(_categoryRelatedProducts.First());
        
        _filteredProducts = _categoryRelatedProducts;
        
        _filteringModel = new ProductSearchFilteringModel();
        _filterCategoryConstants = new FilterCategoryConstants();
        _filterAttributeConstants = new FilterAttributeConstants();

        var extractor = new ProductSpecificationExtractor(
            _categoryRelatedProducts, _filteredProducts,
            _allSpecifications, _manufacturers, _filteringModel,
            _filterCategoryConstants, _filterAttributeConstants);

        var extractedSpecs = extractor.ExtractCommonSpecifications();
        
        var countedSpecs = extractor.ExtractSpecificationsForCounting(extractedSpecs);
        
        Assert.Empty(countedSpecs);
    }
}