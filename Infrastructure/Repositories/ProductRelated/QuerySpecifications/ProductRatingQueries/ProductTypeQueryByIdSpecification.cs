using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductRatingQueries;

public sealed class ProductRatingQueryByIdSpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQueryByIdSpecification(Guid ratingId) 
        : base (criteria => criteria.Id == ratingId) { }
}