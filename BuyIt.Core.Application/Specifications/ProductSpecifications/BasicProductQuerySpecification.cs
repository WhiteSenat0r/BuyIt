using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Entities.ProductRelated;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications.ProductSpecifications;

public abstract class BasicProductQuerySpecification : QuerySpecification<Product>
{
    protected BasicProductQuerySpecification()
    {
        Includes.Add(p =>
            p.Include(m => m.Manufacturer)
                .Include(t => t.ProductType)
                .Include(r => r.Rating));
    }
    
    protected BasicProductQuerySpecification(Expression<Func<Product, bool>> criteria) 
        : base(criteria)
    {
        Includes.Add(p =>
            p.Include(m => m.Manufacturer)
                .Include(t => t.ProductType)
                .Include(r => r.Rating));
    }
}