using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;

public sealed class ProductManufacturerByProductTypeQuerySpecification :
    BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerByProductTypeQuerySpecification(
        string category) 
        : base(criteria => criteria.Products.Any(
            product => product.ProductType.Name.ToLower()
                .Equals(category.ToLower()))) =>
        AddOrderByAscending(m => m.Name);
}