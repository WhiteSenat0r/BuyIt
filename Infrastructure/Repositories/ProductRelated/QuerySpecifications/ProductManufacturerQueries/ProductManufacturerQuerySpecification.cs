using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;

public sealed class ProductManufacturerQuerySpecification : QuerySpecification<ProductManufacturer>
{
    public ProductManufacturerQuerySpecification() =>
        AddOrderByAscending(m => m.Name);
}