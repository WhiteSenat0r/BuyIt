using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications;

public sealed class ProductQueryByManufacturerIdSpecification : BasicProductQuerySpecification
{
    public ProductQueryByManufacturerIdSpecification(Guid manufacturerId) 
        : base(criteria => criteria.ManufacturerId == manufacturerId) => 
        AddOrderByAscending(p => p.Name);
}