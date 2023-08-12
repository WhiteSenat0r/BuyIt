using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRatingRepository : GenericRepository<ProductRating>
{
    internal ProductRatingRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;

    public override async Task<ProductRating?> GetSingleEntityAsync
        (Guid entityId) => await Context.ProductRatings.SingleOrDefaultAsync
            (r => r.Id == entityId);
}