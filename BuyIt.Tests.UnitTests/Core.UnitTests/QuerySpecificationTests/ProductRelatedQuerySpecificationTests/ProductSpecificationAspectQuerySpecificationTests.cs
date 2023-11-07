using System.Linq.Expressions;
using Application.Specifications;
using Domain.Entities;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests;

public class ProductSpecificationAspectQuerySpecificationTests
{
    [Fact]
    public void Constructor_ShouldSetCriteriaAndOrderByExpression()
    {
        Expression<Func<ProductSpecificationCategory, bool>> criteria = aspect => aspect.Value == "Test";
        
        var specification = new ProductSpecificationAspectQuerySpecification<ProductSpecificationCategory>(criteria);
        
        Assert.Equal(criteria, specification.Criteria);
    }
}