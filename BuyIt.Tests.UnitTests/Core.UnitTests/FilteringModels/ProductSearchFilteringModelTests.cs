using Application.FilteringModels;
using Application.Specifications.ProductSpecifications;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.FilteringModels;

public class ProductSearchFilteringModelTests
{
    [Fact]
    public void Constructor_InitializesPropertiesCorrectly()
    {
        var filteringModel = new ProductSearchFilteringModel();

        Assert.NotNull(filteringModel);
    }

    [Fact]
    public void Text_Property_SetAndGetCorrectly()
    {
        var filteringModel = new ProductSearchFilteringModel();
        var textValue = "Sample Text";

        filteringModel.Text = textValue;

        Assert.Equal(textValue, filteringModel.Text);
    }
    
    [Fact]
    public void InStock_Property_SetAndGetCorrectly()
    {
        var filteringModel = new ProductSearchFilteringModel();
        var textValue = "Sample Text";

        filteringModel.InStock = textValue;

        Assert.Equal(textValue, filteringModel.InStock);
    }
    
    [Fact]
    public void SortingType_Property_SetAndGetCorrectly()
    {
        var filteringModel = new ProductSearchFilteringModel();
        var textValue = "Sample Text";

        filteringModel.SortingType = textValue;

        Assert.Equal(textValue, filteringModel.SortingType);
    }
    
    [Fact]
    public void PriceLimit_Properties_SetAndGetCorrectly()
    {
        var filteringModel = new ProductSearchFilteringModel();
        var value = 1m;

        filteringModel.LowerPriceLimit = value;
        filteringModel.UpperPriceLimit = value;

        Assert.Equal(value, filteringModel.LowerPriceLimit);
        Assert.Equal(value, filteringModel.UpperPriceLimit);
    }
    
    [Fact]
    public void ItemQuantity_Property_SetAndGetCorrectly()
    {
        var filteringModel = new ProductSearchFilteringModel();
        var value = 1;

        filteringModel.ItemQuantity = value;

        Assert.Equal(value, filteringModel.ItemQuantity);
    }
    
    [Fact]
    public void CreateQuerySpecification_Method_CreatesRelevantQuerySpecification()
    {
        var filteringModel = new ProductSearchFilteringModel();
        var spec = filteringModel.CreateQuerySpecification();

        Assert.IsType<ProductSearchQuerySpecification>(spec);
    }
}