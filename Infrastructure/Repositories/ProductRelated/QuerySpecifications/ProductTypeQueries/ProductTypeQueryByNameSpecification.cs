using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductTypeQueries;

public class ProductTypeQueryByNameSpecification : QuerySpecification<ProductType>
{
    public ProductTypeQueryByNameSpecification(string name) 
        : base (criteria => criteria.Name.ToLower().Equals(name.ToLower())) { }
}