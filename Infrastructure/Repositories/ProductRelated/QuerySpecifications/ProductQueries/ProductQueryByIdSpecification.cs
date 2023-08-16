using System.Linq.Expressions;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries;

public class ProductQueryByIdSpecification : QuerySpecification<Product>
{
    public ProductQueryByIdSpecification(Guid productId) 
        : base(criteria => criteria.Id == productId)
    {
        AddIncludeRange
        (new List<Expression<Func<Product, object>>>
        {
            p => p.Manufacturer,
            p => p.ProductType,
            p => p.Rating
        });
    }
}