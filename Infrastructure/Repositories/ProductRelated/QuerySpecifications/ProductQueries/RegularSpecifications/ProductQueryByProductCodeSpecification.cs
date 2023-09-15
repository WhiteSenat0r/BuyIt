using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications;

public sealed class ProductQueryByProductCodeSpecification : BasicProductQuerySpecification
{
    public ProductQueryByProductCodeSpecification(string productCode) 
        : base(criteria => criteria.ProductCode.ToLower().Equals(productCode.ToLower())) =>
        AddOrderByAscending(p => p.Name);
}