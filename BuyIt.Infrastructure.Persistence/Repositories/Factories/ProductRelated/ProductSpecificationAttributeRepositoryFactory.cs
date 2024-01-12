using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.Common.Classes;
using Persistence.Repositories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.ProductRelated;

public class ProductSpecificationAttributeRepositoryFactory : RepositoryFactory<ProductSpecificationAttribute>
{
    public override ProductSpecificationAttributeRepository Create(StoreContext dbContext) =>  new(dbContext);
}