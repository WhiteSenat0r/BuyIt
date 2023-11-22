namespace Application.Specifications.ProductManufacturerSpecifications;

public sealed class ProductManufacturerQueryByIdSpecification : BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerQueryByIdSpecification(Guid manufacturerId)
        : base(criteria => criteria.Id == manufacturerId) { }
}