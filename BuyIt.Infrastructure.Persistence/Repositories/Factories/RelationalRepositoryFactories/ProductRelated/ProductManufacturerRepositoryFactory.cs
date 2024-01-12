using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;

public class ProductManufacturerRepositoryFactory : RepositoryFactory<ProductManufacturer>
{
    public override ProductManufacturerRepository Create(StoreContext dbContext) => new(dbContext);
}