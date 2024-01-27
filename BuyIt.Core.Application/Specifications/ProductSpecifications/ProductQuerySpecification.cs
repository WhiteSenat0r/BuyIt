using System.Linq.Expressions;
using Domain.Entities.ProductRelated;

namespace Application.Specifications.ProductSpecifications;

public sealed class ProductQuerySpecification : BasicProductQuerySpecification
{
    public ProductQuerySpecification(bool isNotTracked = false)
        => IsNotTracked = isNotTracked;

    public ProductQuerySpecification(
        Expression<Func<Product, bool>> expression, bool isNotTracked = false)
        : base(expression) =>
        IsNotTracked = isNotTracked;
}