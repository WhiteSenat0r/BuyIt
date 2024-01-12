using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;

public class ProductRatingRepositoryFactory : RepositoryFactory<ProductRating>
{
    public override ProductRatingRepository Create(StoreContext dbContext) => new(dbContext);
}