using Core.Entities.Product.ProductSpecification;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Factories.Common.Classes;
using Infrastructure.Repositories.ProductRelated.ProductSpecificationRelated;

namespace Infrastructure.Repositories.Factories.ProductRelated;

public class ProductSpecificationValueRepositoryFactory : RepositoryFactory<ProductSpecificationValue>
{
    public override ProductSpecificationValueRepository Create(StoreContext dbContext) =>  new(dbContext);
}