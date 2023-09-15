using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductRatingQueries;

public sealed class ProductRatingQueryByScoreLesserThanValueSpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQueryByScoreLesserThanValueSpecification(double? score)
        : base(criteria => criteria.Score < score) 
        => AddOrderByDescending(r => r.Score);
}