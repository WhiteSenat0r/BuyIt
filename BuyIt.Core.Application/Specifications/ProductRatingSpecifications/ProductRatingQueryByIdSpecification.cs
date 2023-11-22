using Application.Specifications.Common;
using Domain.Entities;

namespace Application.Specifications.ProductRatingSpecifications;

public sealed class ProductRatingQueryByIdSpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQueryByIdSpecification(Guid ratingId) 
        : base (criteria => criteria.Id == ratingId) { }
}