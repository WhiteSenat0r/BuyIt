using Application.Specifications;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests;

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
    public void ProductQueryByProductCodeSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductQueryByProductCodeSpecification("A12B32CD");
        
        Assert.IsType<ProductQueryByProductCodeSpecification>(_querySpecification);
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