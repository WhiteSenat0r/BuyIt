using Application.Specifications.Common;
using Domain.Entities;

namespace Application.Specifications.ProductTypeSpecifications;

public sealed class ProductTypeQueryByManufacturerSpecification : QuerySpecification<ProductType>
{
    public ProductTypeQueryByManufacturerSpecification(IEnumerable<string> brandNames) 
        : base(criteria => criteria.Products.Any(
            product => brandNames.Contains(product.Manufacturer.Name))) { }
}