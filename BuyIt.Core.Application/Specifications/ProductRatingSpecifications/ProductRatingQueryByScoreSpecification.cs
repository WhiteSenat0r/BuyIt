using Application.Specifications.Common;
using Domain.Entities.ProductRelated;

namespace Application.Specifications.ProductRatingSpecifications;

public sealed class ProductRatingQueryByScoreSpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQueryByScoreSpecification(double? score)
        : base(criteria => criteria.Score == score) 
        => AddOrderByDescending(r => r.Score);
}