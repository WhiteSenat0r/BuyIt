using Application.Specifications.Common;
using Domain.Entities;

namespace Application.Specifications;

public sealed class ProductRatingQueryByScoreSpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQueryByScoreSpecification(double? score)
        : base(criteria => criteria.Score == score) 
        => AddOrderByDescending(r => r.Score);
}