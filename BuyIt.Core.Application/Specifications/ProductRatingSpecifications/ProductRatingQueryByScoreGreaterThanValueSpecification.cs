using Application.Specifications.Common;
using Domain.Entities.ProductRelated;

namespace Application.Specifications.ProductRatingSpecifications;

public sealed class ProductRatingQueryByScoreGreaterThanValueSpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQueryByScoreGreaterThanValueSpecification(double? score)
        : base(criteria => criteria.Score > score) 
        => AddOrderByDescending(r => r.Score);
}