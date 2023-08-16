using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductRatingQueries;

public class ProductRatingQuerySpecification : QuerySpecification<ProductRating>
{
    public ProductRatingQuerySpecification() => AddOrderByAscending(r => r.Score);
}