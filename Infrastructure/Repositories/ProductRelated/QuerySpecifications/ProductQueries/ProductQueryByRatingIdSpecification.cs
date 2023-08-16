using System.Linq.Expressions;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries;

public class ProductQueryByRatingIdSpecification : QuerySpecification<Product>
{
    public ProductQueryByRatingIdSpecification(Guid ratingId) 
        : base(criteria => criteria.RatingId == ratingId)
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