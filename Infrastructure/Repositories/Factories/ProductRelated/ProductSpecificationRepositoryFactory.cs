using Core.Entities.Product;
using Core.Entities.Product.ProductSpecification;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Factories.Common.Classes;
using Infrastructure.Repositories.ProductRelated;

namespace Infrastructure.Repositories.Factories.ProductRelated;

public class ProductSpecificationRepositoryFactory : RepositoryFactory<ProductSpecification>
{
    public override ProductSpecificationRepository Create(StoreContext dbContext) =>  new(dbContext);
}