using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Entities;

namespace Application.Specifications;

public abstract class BasicProductQuerySpecification : QuerySpecification<Product>
{
    protected BasicProductQuerySpecification()
    {
        AddIncludeRange(new List<Expression<Func<Product, object>>>
        {
            p => p.Manufacturer,
            p => p.ProductType,
            p => p.Rating,
            p => p.Specifications
        });
    }
    
    protected BasicProductQuerySpecification(Expression<Func<Product, bool>> criteria) 
        : base(criteria)
    {
        AddIncludeRange(new List<Expression<Func<Product, object>>>
        {
            p => p.Manufacturer,
            p => p.ProductType,
            p => p.Rating,
            p => p.Specifications
        });
    }
}