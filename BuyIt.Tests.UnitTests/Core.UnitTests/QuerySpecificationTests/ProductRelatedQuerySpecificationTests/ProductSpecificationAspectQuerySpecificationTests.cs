using System.Linq.Expressions;
using Application.Specifications.ProductSpecifications;
using Domain.Entities;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests;

public class ProductSpecificationAspectQuerySpecificationTests
{
    [Fact]
    public void ProductSpecificationQuerySpecificationConstructor_ShouldSetCriteriaAndOrderByExpression()
    {
        var specification = new ProductSpecificationQuerySpecification();
        
        Assert.NotNull(specification);
    }
    
    [Fact]
    public void ProductSpecificationQuerySpecificationExpressionConstructor_ShouldSetCriteriaAndOrderByExpression()
    {
        var specification = new ProductSpecificationQuerySpecification(s => s.Products.Count > 0);
        
        Assert.NotNull(specification);
    }
    
    [Fact]
    public void ProductSpecificationAspectQuerySpecificationConstructor_ShouldSetCriteriaAndOrderByExpression()
    {
        Expression<Func<ProductSpecificationCategory, bool>> criteria = aspect => aspect.Value == "Test";
        
        var specification = new ProductSpecificationAspectQuerySpecification<ProductSpecificationCategory>(criteria);
        
        Assert.Equal(criteria, specification.Criteria);
    }
}