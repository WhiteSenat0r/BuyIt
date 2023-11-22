namespace Application.Specifications.ProductManufacturerSpecifications;

public sealed class ProductManufacturerQueryByNameSpecification : BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerQueryByNameSpecification(string name)
        : base(criteria => criteria.Name.ToLower().Equals(name.ToLower())) =>
        AddOrderByAscending(m => m.Name);
}