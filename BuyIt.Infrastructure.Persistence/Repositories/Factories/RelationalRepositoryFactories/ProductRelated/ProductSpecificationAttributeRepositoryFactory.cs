using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;

public class ProductSpecificationAttributeRepositoryFactory : RepositoryFactory<ProductSpecificationAttribute>
{
    public override ProductSpecificationAttributeRepository Create(StoreContext dbContext) =>  new(dbContext);
}