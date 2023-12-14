using Application.Specifications.ProductManufacturerSpecifications;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests;

public class ProductManufacturerQuerySpecificationTests
{
    private IQuerySpecification<ProductManufacturer> _querySpecification = null!;

    [Fact]
    public void ProductQuerySpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductManufacturerQuerySpecification();
        
        Assert.IsType<ProductManufacturerQuerySpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductQuerySpecificationByIdConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductManufacturerQueryByIdSpecification(Guid.NewGuid());
        
        Assert.IsType<ProductManufacturerQueryByIdSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductQuerySpecificationByNameConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductManufacturerQueryByNameSpecification("Name");
        
        Assert.IsType<ProductManufacturerQueryByNameSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductQuerySpecificationByProductTypeConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductManufacturerByProductTypeQuerySpecification("Test");
        
        Assert.IsType<ProductManufacturerByProductTypeQuerySpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
}