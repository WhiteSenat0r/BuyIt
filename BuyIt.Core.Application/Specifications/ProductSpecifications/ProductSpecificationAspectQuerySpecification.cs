using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Contracts.ProductRelated;

namespace Application.Specifications.ProductSpecifications;

public sealed class ProductSpecificationAspectQuerySpecification<TEntity> : QuerySpecification<TEntity>
    where TEntity : class, ISpecificationAspect
{
    public ProductSpecificationAspectQuerySpecification(Expression<Func<TEntity, bool>> criteria)
        : base(criteria) => AddOrderByAscending(p => p.Value);
}