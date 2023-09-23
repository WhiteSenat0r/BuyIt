using System.Linq.Expressions;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries.Common.Classes;

public abstract class BaseProductManufacturerQuerySpecification : QuerySpecification<ProductManufacturer>
{
    protected BaseProductManufacturerQuerySpecification() => 
        AddInclude(manufacturer => manufacturer.Products);

    protected BaseProductManufacturerQuerySpecification(
        Expression<Func<ProductManufacturer, bool>> criteria) : base(criteria) =>
        AddInclude(manufacturer => manufacturer.Products);
}