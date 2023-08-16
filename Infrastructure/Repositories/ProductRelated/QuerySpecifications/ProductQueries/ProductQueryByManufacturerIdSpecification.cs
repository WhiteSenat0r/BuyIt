using System.Linq.Expressions;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries;

public class ProductQueryByManufacturerIdSpecification : QuerySpecification<Product>
{
    public ProductQueryByManufacturerIdSpecification(Guid manufacturerId) 
        : base(criteria => criteria.ManufacturerId == manufacturerId)
    {
        AddIncludeRange(new List<Expression<Func<Product, object>>>
        {
            p => p.Manufacturer,
            p => p.ProductType,
            p => p.Rating,
        });
        
        AddOrderByAscending(p => p.Name);
    }
}