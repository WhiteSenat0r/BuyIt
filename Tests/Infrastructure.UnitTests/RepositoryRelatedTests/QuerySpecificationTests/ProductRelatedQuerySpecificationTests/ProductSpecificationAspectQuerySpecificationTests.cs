using System.Linq.Expressions;
using Core.Entities.Product.ProductSpecification.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications.ProductSpecificationRelated;

namespace Tests.Infrastructure.UnitTests.RepositoryRelatedTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests;

public class ProductSpecificationAspectQuerySpecificationTests
{
    [Fact]
    public void Constructor_ShouldSetCriteriaAndOrderByExpression()
    {
        Expression<Func<ISpecificationAspect, bool>> criteria = aspect => aspect.Value == "Test";
        
        var specification = new ProductSpecificationAspectQuerySpecification(criteria);
        
        Assert.Equal(criteria, specification.Criteria);
    }
}