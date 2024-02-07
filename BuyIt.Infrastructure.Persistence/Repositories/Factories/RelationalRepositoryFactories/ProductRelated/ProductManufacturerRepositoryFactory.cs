using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;

internal class ProductManufacturerRepositoryFactory : RepositoryFactory<ProductManufacturer>
{
    internal override ProductManufacturerRepository Create(StoreContext dbContext) => new(dbContext);
}