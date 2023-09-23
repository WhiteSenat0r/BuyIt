using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries.Common.FilteringModels;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;

public sealed class ProductManufacturerByProductTypeQuerySpecification :
    BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerByProductTypeQuerySpecification(
        ProductManufacturerFilteringModel filteringModel) 
        : base(criteria => criteria.Products.Any(
            product => product.ProductType.Name.ToLower()
                .Equals(filteringModel.ProductCategory.ToLower().Replace('_', ' ')))) =>
        AddOrderByAscending(m => m.Name);
}