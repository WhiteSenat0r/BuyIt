using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductTypeQueries;

public class ProductTypeQuerySpecification : QuerySpecification<ProductType>
{
    public ProductTypeQuerySpecification() => AddOrderByAscending(p => p.Name);
}