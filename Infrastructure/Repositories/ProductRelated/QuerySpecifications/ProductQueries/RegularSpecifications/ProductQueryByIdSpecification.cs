using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications;

public sealed class ProductQueryByIdSpecification : BasicProductQuerySpecification
{
    public ProductQueryByIdSpecification(Guid productId) 
        : base(criteria => criteria.Id == productId) => AddOrderByAscending(p => p.Name);
}