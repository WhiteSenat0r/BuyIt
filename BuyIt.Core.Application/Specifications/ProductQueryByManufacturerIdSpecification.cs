namespace Application.Specifications;

public sealed class ProductQueryByManufacturerIdSpecification : BasicProductQuerySpecification
{
    public ProductQueryByManufacturerIdSpecification(Guid manufacturerId) 
        : base(criteria => criteria.ManufacturerId == manufacturerId) => 
        AddOrderByAscending(p => p.Name);
}