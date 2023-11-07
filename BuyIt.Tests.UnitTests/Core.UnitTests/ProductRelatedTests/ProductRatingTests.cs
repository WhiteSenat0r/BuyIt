using Domain.Contracts.ProductRelated;
using Domain.Entities;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.ProductRelatedTests;

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
    public void ScoreProperty_Should_ThrowArgumentExceptionIfValueIsGreaterThanFiveOrLessThanOne
        (int score)
    {
        Assert.Throws<ArgumentException>(() => _productRating = new ProductRating(score));
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void ScoreProperty_Should_EqualReceivedValueIfItIsValid(int score)
    {
        _productRating = new ProductRating(score);
        Assert.Equal(score, _productRating.Score);
    }
    
    [Fact]
    public void ScoreProperty_Should_CalculateRatingAfterNewValueIsAssigned()
    {
        _productRating = new ProductRating
        {
            Score = 4.7
        };

        _productRating.Score = 2;
        
        Assert.Equal(3.4, _productRating.Score);
    }

    [Fact]
    public void ScoreProperty_Should_BeNullIfNullIsPassedAsConstructorArgument()
    {
        _productRating = new ProductRating(null);

        Assert.Null(_productRating.Score);
    }
    
    [Fact]
    public void IdProperty_Should_NotBeEmpty()
    {
        _productRating = new ProductRating();

        Assert.NotEqual(Guid.Empty, _productRating.Id);
    }

    private static ProductRating GetFullyInitializedProductRating() => new(5);
}