using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated;

public class ProductRatingRepository : GenericRepository<ProductRating>
{
    internal ProductRatingRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}