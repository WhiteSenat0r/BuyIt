using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Entities.ProductRelated;

namespace Application.Specifications.ProductTypeSpecifications;

public sealed class ProductTypeQuerySpecification : QuerySpecification<ProductType>
{
    public ProductTypeQuerySpecification() => AddOrderByAscending(p => p.Name);

    public ProductTypeQuerySpecification(Expression<Func<ProductType, bool>> expression)
        : base(expression) =>
        AddInclude("Products");
}