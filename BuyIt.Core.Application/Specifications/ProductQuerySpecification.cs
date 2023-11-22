using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Specifications;

public sealed class ProductQuerySpecification : BasicProductQuerySpecification
{
    public ProductQuerySpecification() { }

    public ProductQuerySpecification(Expression<Func<Product, bool>> expression) : base(expression) { }
}