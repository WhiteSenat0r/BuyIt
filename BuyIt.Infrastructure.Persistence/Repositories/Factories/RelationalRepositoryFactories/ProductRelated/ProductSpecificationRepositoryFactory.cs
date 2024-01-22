using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;

public class ProductSpecificationRepositoryFactory : RepositoryFactory<ProductSpecification>
{
    public override ProductSpecificationRepository Create(StoreContext dbContext) =>  new(dbContext);
}