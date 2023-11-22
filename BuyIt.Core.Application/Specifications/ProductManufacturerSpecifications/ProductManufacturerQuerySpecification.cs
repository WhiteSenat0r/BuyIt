namespace Application.Specifications.ProductManufacturerSpecifications;

public sealed class ProductManufacturerQuerySpecification : BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerQuerySpecification() =>
        AddOrderByAscending(m => m.Name);
}