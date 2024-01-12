using Application.Specifications.Common;
using Domain.Entities.ProductRelated;

namespace Application.Specifications.ProductRatingSpecifications;

public sealed class ProductRatingQuerySpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQuerySpecification() => AddOrderByAscending(r => r.Score);
}