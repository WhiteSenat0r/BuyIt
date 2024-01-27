namespace Application.Specifications.ProductManufacturerSpecifications;

public sealed class ProductManufacturerQuerySpecification : BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerQuerySpecification(bool isNotTracked = false)
    {
        IsNotTracked = isNotTracked;
        
        AddOrderByAscending(m => m.Name);
    }
}