namespace Application.Specifications;

public sealed class ProductManufacturerQuerySpecification : BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerQuerySpecification() =>
        AddOrderByAscending(m => m.Name);
}