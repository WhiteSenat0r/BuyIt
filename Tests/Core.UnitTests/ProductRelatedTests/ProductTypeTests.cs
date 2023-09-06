using Core.Entities.Product;
using Core.Entities.Product.Common.Interfaces;

namespace Tests.Core.UnitTests.ProductRelatedTests;

public class ProductTypeTests
{
    private IProductType _productType = null!;
    
    [Fact]
    public void EmptyConstructor_Should_CreateNewObject()
    {
        _productType = new ProductType();
        
        Assert.True(_productType.GetType() == typeof(ProductType));
    }
    
    [Fact]
    public void FullDataConstructor_Should_CreateNewFullyInitializedObject()
    {
        _productType = GetFullyInitializedProductType();

        Assert.True(_productType.GetType() == typeof(ProductType));
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

    private static ProductType GetFullyInitializedProductType() =>
        new ("Type");
}