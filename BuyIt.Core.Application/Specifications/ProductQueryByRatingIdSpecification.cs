namespace Application.Specifications;

public sealed class ProductQueryByRatingIdSpecification : BasicProductQuerySpecification
{
    public ProductQueryByRatingIdSpecification(Guid ratingId) 
        : base(criteria => criteria.RatingId == ratingId) =>
        AddOrderByAscending(p => p.Name);
}