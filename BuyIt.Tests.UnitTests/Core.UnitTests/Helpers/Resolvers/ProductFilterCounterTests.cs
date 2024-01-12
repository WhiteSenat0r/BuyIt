using Application.Contracts;
using Application.FilteringModels;
using Application.Helpers.SpecificationResolver.Common;
using Application.Helpers.SpecificationResolver.Common.Constants;
using Domain.Entities.ProductRelated;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.Helpers.Resolvers;

public class ProductFilterCounterTests
{
    private IEnumerable<Product> _categoryRelatedProducts;
    private IEnumerable<Product> _filteredProducts;
    private IEnumerable<ProductSpecification> _allSpecifications;
    private IEnumerable<ProductManufacturer> _manufacturers;
    private IEnumerable<ProductType> _categories;
    private IFilteringModel _filteringModel;
    private FilterCategoryConstants _filterCategoryConstants;
    private FilterAttributeConstants _filterAttributeConstants;
    private ProductSpecificationExtractor _productSpecificationExtractor;
    
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

        _productSpecificationExtractor = new ProductSpecificationExtractor(
            _categoryRelatedProducts, _filteredProducts,
            _allSpecifications, _manufacturers, _filteringModel,
            _filterCategoryConstants, _filterAttributeConstants);

        var productSpecificationCounter = new ProductFilterCounter(
            _categoryRelatedProducts, _filteredProducts,
            _filteredProducts,
            new List<ProductSpecification>(),
            _manufacturers,
            new List<ProductType>(), 
            new ProductSearchFilteringModel(), _productSpecificationExtractor);
        
        Assert.NotNull(productSpecificationCounter);
    }
    
    [Fact]
    public void GetCountedBrands_Method_CountsBrandsCorrectly()
    {
        _manufacturers = new List<ProductManufacturer> { new("TestM") };
        _categories = new List<ProductType> { new("Personal computer") };
    
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
                _categories.First(),
                new ProductRating(), new [] { "1.jpg", "2.jpg" } ),
            new ("Test", "Test", 1m,
                true, _manufacturers.First(),
                _categories.First(),
                new ProductRating(), new [] { "1.jpg", "2.jpg" } )
        };
        
        _categoryRelatedProducts.First().Specifications.Add(_allSpecifications.First());
        _categoryRelatedProducts.First().Specifications.Add(_allSpecifications.Last());
        _categoryRelatedProducts.Last().Specifications.Add(_allSpecifications.First());

        _categoryRelatedProducts.First().Manufacturer = _manufacturers.First();
        _categoryRelatedProducts.Last().Manufacturer = _manufacturers.First();
        
        _categoryRelatedProducts.First().ProductType = _categories.First();
        _categoryRelatedProducts.Last().ProductType = _categories.First();
        
        _manufacturers.First().Products.Add(_categoryRelatedProducts.First());
        _manufacturers.First().Products.Add(_categoryRelatedProducts.Last());
        
        _categories.First().Products.Add(_categoryRelatedProducts.First());
        _categories.First().Products.Add(_categoryRelatedProducts.Last());
        
        _filteredProducts = _categoryRelatedProducts;
        
        _filteringModel = new ProductSearchFilteringModel();
        _filterCategoryConstants = new FilterCategoryConstants();
        _filterAttributeConstants = new FilterAttributeConstants();
    
        _productSpecificationExtractor = new ProductSpecificationExtractor(
            _categoryRelatedProducts, _filteredProducts,
            _allSpecifications, _manufacturers, _filteringModel,
            _filterCategoryConstants, _filterAttributeConstants);
    
        var extractedSpecs = _productSpecificationExtractor.ExtractCommonSpecifications();
        
        var productSpecificationCounter = new ProductFilterCounter(
            _categoryRelatedProducts, _filteredProducts,
            _filteredProducts,
            extractedSpecs,
            _manufacturers,
            _categories, 
            new PersonalComputerFilteringModel(), _productSpecificationExtractor);
        
        Assert.NotEmpty(productSpecificationCounter.GetCountedBrands());
    }
    
    [Fact]
    public void GetCountedCategories_Method_CountsCategoriesCorrectly()
    {
        _manufacturers = new List<ProductManufacturer> { new("TestM") };
        _categories = new List<ProductType> { new("Personal computer") };
    
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
                _categories.First(),
                new ProductRating(), new [] { "1.jpg", "2.jpg" } ),
            new ("Test", "Test", 1m,
                true, _manufacturers.First(),
                _categories.First(),
                new ProductRating(), new [] { "1.jpg", "2.jpg" } )
        };
        
        _categoryRelatedProducts.First().Specifications.Add(_allSpecifications.First());
        _categoryRelatedProducts.First().Specifications.Add(_allSpecifications.Last());
        _categoryRelatedProducts.Last().Specifications.Add(_allSpecifications.First());

        _categoryRelatedProducts.First().Manufacturer = _manufacturers.First();
        _categoryRelatedProducts.Last().Manufacturer = _manufacturers.First();
        
        _categoryRelatedProducts.First().ProductType = _categories.First();
        _categoryRelatedProducts.Last().ProductType = _categories.First();
        
        _manufacturers.First().Products.Add(_categoryRelatedProducts.First());
        _manufacturers.First().Products.Add(_categoryRelatedProducts.Last());
        
        _categories.First().Products.Add(_categoryRelatedProducts.First());
        _categories.First().Products.Add(_categoryRelatedProducts.Last());
        
        _filteredProducts = _categoryRelatedProducts;
        
        _filteringModel = new ProductSearchFilteringModel();
        _filterCategoryConstants = new FilterCategoryConstants();
        _filterAttributeConstants = new FilterAttributeConstants();
    
        _productSpecificationExtractor = new ProductSpecificationExtractor(
            _categoryRelatedProducts, _filteredProducts,
            _allSpecifications, _manufacturers, _filteringModel,
            _filterCategoryConstants, _filterAttributeConstants);
    
        var extractedSpecs = _productSpecificationExtractor.ExtractCommonSpecifications();
        
        var productSpecificationCounter = new ProductFilterCounter(
            _categoryRelatedProducts, _filteredProducts,
            _filteredProducts,
            extractedSpecs,
            _manufacturers,
            _categories, 
            new ProductSearchFilteringModel(), _productSpecificationExtractor);
        
        Assert.NotEmpty(productSpecificationCounter.GetCountedCategories());
    }
    
    [Fact]
    public void GetCountedGetCountedSpecifications_Method_CountsCategoriesCorrectly()
    {
        _manufacturers = new List<ProductManufacturer> { new("TestM") };
        _categories = new List<ProductType> { new("Personal computer") };
    
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
                _categories.First(),
                new ProductRating(), new [] { "1.jpg", "2.jpg" } ),
            new ("Test", "Test", 1m,
                true, _manufacturers.First(),
                _categories.First(),
                new ProductRating(), new [] { "1.jpg", "2.jpg" } )
        };
        
        _categoryRelatedProducts.First().Specifications.Add(_allSpecifications.First());
        _categoryRelatedProducts.First().Specifications.Add(_allSpecifications.Last());
        _categoryRelatedProducts.Last().Specifications.Add(_allSpecifications.First());

        _categoryRelatedProducts.First().Manufacturer = _manufacturers.First();
        _categoryRelatedProducts.Last().Manufacturer = _manufacturers.First();
        
        _categoryRelatedProducts.First().ProductType = _categories.First();
        _categoryRelatedProducts.Last().ProductType = _categories.First();
        
        _manufacturers.First().Products.Add(_categoryRelatedProducts.First());
        _manufacturers.First().Products.Add(_categoryRelatedProducts.Last());
        
        _categories.First().Products.Add(_categoryRelatedProducts.First());
        _categories.First().Products.Add(_categoryRelatedProducts.Last());
        
        _filteredProducts = _categoryRelatedProducts;
        
        _filteringModel = new ProductSearchFilteringModel();
        _filterCategoryConstants = new FilterCategoryConstants();
        _filterAttributeConstants = new FilterAttributeConstants();
    
        _productSpecificationExtractor = new ProductSpecificationExtractor(
            _categoryRelatedProducts, _filteredProducts,
            _allSpecifications, _manufacturers, _filteringModel,
            _filterCategoryConstants, _filterAttributeConstants);
    
        var extractedSpecs = _productSpecificationExtractor.ExtractCommonSpecifications();
        
        var productSpecificationCounter = new ProductFilterCounter(
            _categoryRelatedProducts, _filteredProducts,
            _filteredProducts,
            extractedSpecs,
            _manufacturers,
            _categories, 
            new PersonalComputerFilteringModel(), _productSpecificationExtractor);
        
        Assert.Empty(productSpecificationCounter.GetCountedSpecifications());
    }
}