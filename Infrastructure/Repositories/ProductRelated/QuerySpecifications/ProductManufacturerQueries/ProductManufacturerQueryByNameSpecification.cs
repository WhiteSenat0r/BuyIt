using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;

public sealed class ProductManufacturerQueryByNameSpecification : BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerQueryByNameSpecification(string name)
        : base(criteria => criteria.Name.ToLower().Equals(name.ToLower())) =>
        AddOrderByAscending(m => m.Name);
}