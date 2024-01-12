using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.Common.Classes;
using Persistence.Repositories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.ProductRelated;

public class ProductSpecificationValueRepositoryFactory : RepositoryFactory<ProductSpecificationValue>
{
    public override ProductSpecificationValueRepository Create(StoreContext dbContext) =>  new(dbContext);
}