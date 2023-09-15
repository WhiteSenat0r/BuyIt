using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;

public sealed class ProductManufacturerQueryByIdSpecification : QuerySpecification<ProductManufacturer>
{
    public ProductManufacturerQueryByIdSpecification(Guid manufacturerId)
        : base(criteria => criteria.Id == manufacturerId) { }
}