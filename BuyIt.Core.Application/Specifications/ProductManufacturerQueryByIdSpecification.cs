namespace Application.Specifications;

public sealed class ProductManufacturerQueryByIdSpecification : BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerQueryByIdSpecification(Guid manufacturerId)
        : base(criteria => criteria.Id == manufacturerId) { }
}