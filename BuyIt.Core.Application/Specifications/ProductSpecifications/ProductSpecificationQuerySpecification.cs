using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Entities.ProductRelated;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications.ProductSpecifications;

public sealed class ProductSpecificationQuerySpecification : QuerySpecification<ProductSpecification>
{
    public ProductSpecificationQuerySpecification()
    {
        Includes.Add(s => 
            s.Include(c => c.SpecificationCategory)
                .Include(a => a.SpecificationAttribute)
                .Include(v => v.SpecificationValue)
                .Include(p => p.Products));
    }
    
    public ProductSpecificationQuerySpecification(Expression<Func<ProductSpecification, bool>> criteria)
        : base(criteria)
    {
        Includes.Add(s => 
            s.Include(c => c.SpecificationCategory)
                .Include(a => a.SpecificationAttribute)
                .Include(v => v.SpecificationValue)
                .Include(p => p.Products));
    }
}