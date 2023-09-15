using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;

public sealed class ProductManufacturerQueryByNameSpecification : QuerySpecification<ProductManufacturer>
{
    public ProductManufacturerQueryByNameSpecification(string name)
        : base(criteria => criteria.Name.ToLower().Equals(name.ToLower())) =>
        AddOrderByAscending(m => m.Name);
}