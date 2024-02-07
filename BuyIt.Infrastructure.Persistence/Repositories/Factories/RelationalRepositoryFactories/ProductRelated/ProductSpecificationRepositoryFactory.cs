using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;

internal class ProductSpecificationRepositoryFactory : RepositoryFactory<ProductSpecification>
{
    internal override ProductSpecificationRepository Create(StoreContext dbContext) =>  new(dbContext);
}