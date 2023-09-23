using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;

public sealed class ProductManufacturerQuerySpecification : BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerQuerySpecification() =>
        AddOrderByAscending(m => m.Name);
}