using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries;

namespace Tests.Infrastructure.UnitTests.RepositoryRelatedTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests;

public class ProductQuerySpecificationTests
{
    private IQuerySpecification<Product> _querySpecification = null!;

    [Fact]
    public void ProductQuerySpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductQuerySpecification();
        
        Assert.IsType<ProductQuerySpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductQueryByIdSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductQueryByIdSpecification(Guid.NewGuid());
        
        Assert.IsType<ProductQueryByIdSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductQueryByRatingIdSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductQueryByRatingIdSpecification(Guid.NewGuid());
        
        Assert.IsType<ProductQueryByRatingIdSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductQueryByManufacturerIdSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductQueryByManufacturerIdSpecification(Guid.NewGuid());
        
        Assert.IsType<ProductQueryByManufacturerIdSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
}