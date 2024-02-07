using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;

internal class ProductRepositoryFactory : RepositoryFactory<Product>
{
    internal override ProductRepository Create(StoreContext dbContext) => new(dbContext);
}