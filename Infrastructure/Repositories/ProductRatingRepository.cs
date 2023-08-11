using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories;

internal class ProductRatingRepository : GenericRepository<ProductRating>
{
    public ProductRatingRepository
        (StoreContext dbContext) : base(dbContext)
    {
        Context = dbContext;
    }
}