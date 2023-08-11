using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories;

internal class ProductManufacturerRepository : GenericRepository<ProductManufacturer>
{
    public ProductManufacturerRepository
        (StoreContext dbContext) : base(dbContext)
    {
        Context = dbContext;
    }
}