using Core.Entities.Product;
using Core.Entities.Product.Common.Interfaces;

namespace Tests.ProductRelatedTests;

public class ProductRatingTests
{
    private IProductRating _productRating = null!;
    
    [Fact]
    public void EmptyConstructor_Should_CreateNewObject()
    {
        _productRating = new ProductRating();
        
        Assert.True(_productRating.GetType() == typeof(ProductRating));
    }
    
    [Fact]
    public void FullDataConstructor_Should_CreateNewFullyInitializedObject()
    {
        _productRating = GetFullyInitializedProductRating();

        Assert.True(_productRating.GetType() == typeof(ProductRating));
        Assert.True(_productRating.Score is not null);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public void ScoreProperty_Should_ThrowArgumentExceptionIfValueIsGreaterThanFiveOrLessThanOne(int score)
    {
        Assert.Throws<ArgumentException>(() => _productRating = new ProductRating(score));
    }
    
    [Fact]
    public void ScoreProperty_Should_CalculateRatingAfterNewValueIsAssigned()
    {
        _productRating = new ProductRating()
        {
            Score = 4.7
        };

        _productRating.Score = 2;
        
        Assert.Equal(3.4, _productRating.Score);
    }

    [Fact]
    public void IdProperty_Should_NotBeEmpty()
    {
        _productRating = new ProductRating();

        Assert.NotEqual(Guid.Empty, _productRating.Id);
    }
    
    [Fact]
    public void IdProperty_Should_BeAbleToSetNewGuid()
    {
        _productRating = new ProductRating();

        var initialProductId = _productRating.Id;

        _productRating.Id = Guid.NewGuid();
        
        Assert.NotEqual(initialProductId, _productRating.Id);
    }
    
    [Fact]
    public void ProductProperty_Should_NotBeInitializedByDefault()
    {
        _productRating = new ProductRating();

        Assert.True(_productRating.Product is null);
    }
    
    [Fact]
    public void ProductProperty_Should_BeAbleToSetNewProduct()
    {
        _productRating = new ProductRating();

        var initialProduct = _productRating.Product;

        _productRating.Product = new Product();
        
        Assert.NotEqual(initialProduct, _productRating.Product);
    }
    
    private static ProductRating GetFullyInitializedProductRating() => new(5);
}