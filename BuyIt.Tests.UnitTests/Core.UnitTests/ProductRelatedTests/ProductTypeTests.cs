using Domain.Contracts.ProductRelated;
using Domain.Entities;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.ProductRelatedTests;

public class ProductTypeTests
{
    private IProductType _productType = null!;
    
    [Fact]
    public void EmptyConstructor_Should_CreateNewObject()
    {
        _productType = new ProductType();
        
        Assert.True(_productType is ProductType);
    }
    
    [Fact]
    public void FullDataConstructor_Should_CreateNewFullyInitializedObject()
    {
        _productType = GetFullyInitializedProductType();

        Assert.True(_productType is ProductType);
        Assert.True(_productType.Name is not null);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void NameProperty_Should_ThrowArgumentNullExceptionIfNullOrEmpty(string value)
    {
        Assert.Throws<ArgumentNullException>(() => _productType = new ProductType(value));
    }

    [Fact]
    public void IdProperty_Should_NotBeEmpty()
    {
        _productType = new ProductType();

        Assert.NotEqual(Guid.Empty, _productType.Id);
    }
    
    [Fact]
    public void IdProperty_Should_BeAbleToSetNewValue()
    {
        _productType = new ProductType();

        var guid = Guid.NewGuid();

        _productType.Id = guid;

        Assert.NotEqual(Guid.Empty, _productType.Id);
    }

    [Fact]
    public void ValueProperty_Should_BeAbleToSetNewValue()
    {
        _productType = new ProductType();

        var products = new Product[] { new(), new() };

        _productType.Products = products;

        Assert.NotEmpty(_productType.Products);
    }
    
    private static ProductType GetFullyInitializedProductType() =>
        new ("Type");
}