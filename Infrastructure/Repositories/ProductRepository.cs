using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories;

internal class ProductRepository : GenericRepository<Product>
{
    public ProductRepository
        (StoreContext dbContext) : base(dbContext)
    {
        Context = dbContext;
    }
}