using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductRatingQueries;

public sealed class ProductRatingQuerySpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQuerySpecification() => AddOrderByAscending(r => r.Score);
}