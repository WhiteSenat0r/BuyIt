using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Entities.ProductRelated;

namespace Application.Specifications.ProductSpecifications;

public abstract class BasicProductQuerySpecification : QuerySpecification<Product>
{
    protected BasicProductQuerySpecification()
    {
        AddInclude("Manufacturer");
        AddInclude("ProductType");
        AddInclude("Rating");
        AddInclude("Specifications");
    }
    
    protected BasicProductQuerySpecification(Expression<Func<Product, bool>> criteria) 
        : base(criteria)
    {
        AddInclude("Manufacturer");
        AddInclude("ProductType");
        AddInclude("Rating");
        AddInclude("Specifications");
    }
}