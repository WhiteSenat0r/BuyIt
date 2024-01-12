using Application.Specifications.Common;
using Domain.Entities.ProductRelated;

namespace Application.Specifications.ProductRatingSpecifications;

public sealed class ProductRatingQueryByScoreLesserThanValueSpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQueryByScoreLesserThanValueSpecification(double? score)
        : base(criteria => criteria.Score < score) 
        => AddOrderByDescending(r => r.Score);
}