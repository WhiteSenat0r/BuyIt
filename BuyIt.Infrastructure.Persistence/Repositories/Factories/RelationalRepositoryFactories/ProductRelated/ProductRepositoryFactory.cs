using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;

public class ProductRepositoryFactory : RepositoryFactory<Product>
{
    public override ProductRepository Create(StoreContext dbContext) => new(dbContext);
}