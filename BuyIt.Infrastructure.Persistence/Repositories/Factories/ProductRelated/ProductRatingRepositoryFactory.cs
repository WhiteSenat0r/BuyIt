using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Factories.Common.Classes;
using Persistence.Repositories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.ProductRelated;

public class ProductRatingRepositoryFactory : RepositoryFactory<ProductRating>
{
    public override ProductRatingRepository Create(StoreContext dbContext) => new(dbContext);
}