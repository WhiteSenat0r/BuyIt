using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.ProductRelatedRepositories;

public sealed class ProductRatingRepository : GenericRepository<ProductRating>
{
    internal ProductRatingRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}