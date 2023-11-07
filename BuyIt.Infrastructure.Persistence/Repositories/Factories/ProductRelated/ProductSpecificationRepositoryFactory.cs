using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Factories.Common.Classes;
using Persistence.Repositories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.ProductRelated;

public class ProductSpecificationRepositoryFactory : RepositoryFactory<ProductSpecification>
{
    public override ProductSpecificationRepository Create(StoreContext dbContext) =>  new(dbContext);
}