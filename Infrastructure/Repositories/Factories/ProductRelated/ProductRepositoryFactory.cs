using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Factories.Common.Classes;
using Infrastructure.Repositories.ProductRelated;

namespace Infrastructure.Repositories.Factories.ProductRelated;

public class ProductRepositoryFactory : RepositoryFactory<Product>
{
    public override ProductRepository Create(StoreContext dbContext) => new(dbContext);
}