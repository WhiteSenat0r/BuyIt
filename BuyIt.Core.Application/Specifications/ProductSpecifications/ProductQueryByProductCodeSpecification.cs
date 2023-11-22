using Microsoft.EntityFrameworkCore;

namespace Application.Specifications.ProductSpecifications;

public sealed class ProductQueryByProductCodeSpecification : BasicProductQuerySpecification
{
    public ProductQueryByProductCodeSpecification(string productCode) 
        : base(criteria => criteria.ProductCode.ToLower().Equals(productCode.ToLower()))
    {
        Includes.Add(p =>
            p.Include(m => m.Manufacturer)
                .Include(t => t.ProductType)
                .Include(r => r.Rating)
                .Include(s => s.Specifications)
                .ThenInclude(c => c.SpecificationCategory)
                .Include(s => s.Specifications)
                .ThenInclude(a => a.SpecificationAttribute)
                .Include(s => s.Specifications)
                .ThenInclude(v => v.SpecificationValue));
    }
}