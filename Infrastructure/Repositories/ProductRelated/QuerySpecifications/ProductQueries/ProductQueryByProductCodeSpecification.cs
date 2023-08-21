using System.Linq.Expressions;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries;

public class ProductQueryByProductCodeSpecification : QuerySpecification<Product>
{
    public ProductQueryByProductCodeSpecification(string productCode) 
        : base(criteria => criteria.ProductCode.ToLower().Equals(productCode.ToLower()))
    {
        AddIncludeRange
        (new List<Expression<Func<Product, object>>>
        {
            p => p.Manufacturer,
            p => p.ProductType,
            p => p.Rating
        });
        
        AddOrderByAscending(p => p.Name);
    }
}