using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Factories.Common.Classes;

namespace Infrastructure.Repositories.Factories;

public class ProductRatingRepositoryFactory : RepositoryFactory<ProductRating>
{
    public override ProductRatingRepository Create(StoreContext dbContext) => new(dbContext);
}