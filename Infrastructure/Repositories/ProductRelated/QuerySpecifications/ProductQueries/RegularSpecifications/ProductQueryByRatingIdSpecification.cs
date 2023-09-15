using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications;

public sealed class ProductQueryByRatingIdSpecification : BasicProductQuerySpecification
{
    public ProductQueryByRatingIdSpecification(Guid ratingId) 
        : base(criteria => criteria.RatingId == ratingId) =>
        AddOrderByAscending(p => p.Name);
}