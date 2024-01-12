using System.Linq.Expressions;
using Domain.Entities.ProductRelated;

namespace Application.Specifications.ProductSpecifications;

public sealed class ProductQuerySpecification : BasicProductQuerySpecification
{
    public ProductQuerySpecification() { }

    public ProductQuerySpecification(Expression<Func<Product, bool>> expression) : base(expression) { }
}