using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductRatingQueries;

public sealed class ProductRatingQueryByScoreGreaterThanValueSpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQueryByScoreGreaterThanValueSpecification(double? score)
        : base(criteria => criteria.Score > score) 
        => AddOrderByDescending(r => r.Score);
}