using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductTypeQueries;

public sealed class ProductTypeQueryByManufacturerSpecification : QuerySpecification<ProductType>
{
    public ProductTypeQueryByManufacturerSpecification(IEnumerable<string> brandNames) 
        : base(criteria => criteria.Products.Any(
            product => brandNames.Contains(product.Manufacturer.Name))) { }
}