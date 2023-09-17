using System.Linq.Expressions;
using Core.Entities.Product.ProductSpecification.Common.Interfaces;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications.ProductSpecificationRelated;

public sealed class ProductSpecificationAspectQuerySpecification : QuerySpecification<ISpecificationAspect>
{
    public ProductSpecificationAspectQuerySpecification(Expression<Func<ISpecificationAspect, bool>> criteria)
        : base(criteria) => AddOrderByAscending(p => p.Value);
}