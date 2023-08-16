using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductTypeQueries;

public class ProductTypeQueryByIdSpecification : QuerySpecification<ProductType>
{
    public ProductTypeQueryByIdSpecification(Guid productTypeId) 
        : base (criteria => criteria.Id == productTypeId) { }
}