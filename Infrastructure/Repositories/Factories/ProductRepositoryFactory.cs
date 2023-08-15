using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Factories.Common.Classes;

namespace Infrastructure.Repositories.Factories;

public class ProductRepositoryFactory : RepositoryFactory<Product>
{
    public override ProductRepository Create(StoreContext dbContext) => new(dbContext);
}