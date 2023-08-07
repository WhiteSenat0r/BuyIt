using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.Factories.Common.Classes;

namespace Infrastructure.Repositories.Factories;

public class ProductRatingRepositoryFactory : RepositoryFactory<ProductRating>
{
    public override IRepository<ProductRating> Create(StoreContext dbContext) => 
        new ProductRatingRepository(dbContext);
}