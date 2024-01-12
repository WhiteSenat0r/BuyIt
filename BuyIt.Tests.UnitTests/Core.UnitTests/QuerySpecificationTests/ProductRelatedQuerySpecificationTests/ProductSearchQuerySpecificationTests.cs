using Application.FilteringModels;
using Application.Specifications.ProductSpecifications;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities.ProductRelated;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests;

public class ProductSearchQuerySpecificationTests
{
    private IQuerySpecification<Product> _querySpecification = null!;

    [Fact]
    public void ProductQuerySpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductSearchQuerySpecification(new ProductSearchFilteringModel());
        
        Assert.IsType<ProductSearchQuerySpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
}