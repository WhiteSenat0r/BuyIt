using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Entities.ProductRelated;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications.ProductManufacturerSpecifications;

public abstract class BaseProductManufacturerQuerySpecification : QuerySpecification<ProductManufacturer>
{
    protected BaseProductManufacturerQuerySpecification() => 
        Includes.Add(manufacturer => manufacturer.Include(p => p.Products));

    protected BaseProductManufacturerQuerySpecification(
        Expression<Func<ProductManufacturer, bool>> criteria) : base(criteria) =>
        Includes.Add(manufacturer => manufacturer.Include(p => p.Products));
}