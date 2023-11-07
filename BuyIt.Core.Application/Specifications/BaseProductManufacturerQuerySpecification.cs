using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Entities;


namespace Application.Specifications;

public abstract class BaseProductManufacturerQuerySpecification : QuerySpecification<ProductManufacturer>
{
    protected BaseProductManufacturerQuerySpecification() => 
        AddInclude(manufacturer => manufacturer.Products);

    protected BaseProductManufacturerQuerySpecification(
        Expression<Func<ProductManufacturer, bool>> criteria) : base(criteria) =>
        AddInclude(manufacturer => manufacturer.Products);
}