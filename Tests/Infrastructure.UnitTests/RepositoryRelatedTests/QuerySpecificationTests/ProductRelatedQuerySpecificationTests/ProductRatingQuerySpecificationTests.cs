using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductRatingQueries;

namespace Tests.Infrastructure.UnitTests.RepositoryRelatedTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests;

public class ProductRatingQuerySpecificationTests
{
    private IQuerySpecification<ProductRating> _querySpecification = null!;

    [Fact]
    public void ProductRatingQuerySpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductRatingQuerySpecification();
        
        Assert.IsType<ProductRatingQuerySpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductRatingQueryByIdSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductRatingQueryByIdSpecification(Guid.NewGuid());
        
        Assert.IsType<ProductRatingQueryByIdSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductRatingQueryByScoreSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductRatingQueryByScoreSpecification(null);
        
        Assert.IsType<ProductRatingQueryByScoreSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductRatingQueryByGreaterScoreSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductRatingQueryByScoreGreaterThanValueSpecification(null);
        
        Assert.IsType<ProductRatingQueryByScoreGreaterThanValueSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductRatingQueryByLesserScoreSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductRatingQueryByScoreLesserThanValueSpecification(null);
        
        Assert.IsType<ProductRatingQueryByScoreLesserThanValueSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
}