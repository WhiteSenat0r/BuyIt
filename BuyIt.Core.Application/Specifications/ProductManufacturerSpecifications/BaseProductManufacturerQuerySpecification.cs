using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Entities.ProductRelated;

namespace Application.Specifications.ProductManufacturerSpecifications;

public abstract class BaseProductManufacturerQuerySpecification : QuerySpecification<ProductManufacturer>
{
    protected BaseProductManufacturerQuerySpecification()
    {
        AddInclude("Products");
        // Includes.Add(manufacturer => manufacturer.Include(p => p.Products));
    }

    protected BaseProductManufacturerQuerySpecification(
        Expression<Func<ProductManufacturer, bool>> criteria) : base(criteria)
    {
        AddInclude("Products");
        // Includes.Add(manufacturer => manufacturer.Include(p => p.Products));
    }
}