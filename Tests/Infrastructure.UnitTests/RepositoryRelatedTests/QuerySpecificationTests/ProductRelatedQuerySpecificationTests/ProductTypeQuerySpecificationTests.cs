using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductTypeQueries;

namespace Tests.Infrastructure.UnitTests.RepositoryRelatedTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests;

public class ProductTypeQuerySpecificationTests
{
    private IQuerySpecification<ProductType> _querySpecification = null!;

    [Fact]
    public void ProductTypeQuerySpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductTypeQuerySpecification();
        
        Assert.IsType<ProductTypeQuerySpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductTypeQueryByIdSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductTypeQueryByIdSpecification(Guid.NewGuid());
        
        Assert.IsType<ProductTypeQueryByIdSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductTypeQueryByNameSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductTypeQueryByNameSpecification("Name");
        
        Assert.IsType<ProductTypeQueryByNameSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
}