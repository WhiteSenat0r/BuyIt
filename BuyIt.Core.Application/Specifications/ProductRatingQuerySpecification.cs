using Application.Specifications.Common;
using Domain.Entities;

namespace Application.Specifications;

public sealed class ProductRatingQuerySpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQuerySpecification() => AddOrderByAscending(r => r.Score);
}