using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;

public sealed class ProductManufacturerQueryByIdSpecification : BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerQueryByIdSpecification(Guid manufacturerId)
        : base(criteria => criteria.Id == manufacturerId) { }
}