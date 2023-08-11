using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories;

internal class ProductTypeRepository : GenericRepository<ProductType>
{
    public ProductTypeRepository
        (StoreContext dbContext) : base(dbContext)
    {
        Context = dbContext;
    }
}