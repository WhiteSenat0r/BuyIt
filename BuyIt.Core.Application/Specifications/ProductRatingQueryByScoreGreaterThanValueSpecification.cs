using Application.Specifications.Common;
using Domain.Entities;

namespace Application.Specifications;

public sealed class ProductRatingQueryByScoreGreaterThanValueSpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQueryByScoreGreaterThanValueSpecification(double? score)
        : base(criteria => criteria.Score > score) 
        => AddOrderByDescending(r => r.Score);
}