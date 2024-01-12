using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.Common.Classes;
using Persistence.Repositories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.ProductRelated;

public class ProductManufacturerRepositoryFactory : RepositoryFactory<ProductManufacturer>
{
    public override ProductManufacturerRepository Create(StoreContext dbContext) => new(dbContext);
}