using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductManufacturerRepository : GenericRepository<ProductManufacturer>
{
    internal ProductManufacturerRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;

    public override async Task<ProductManufacturer?> GetSingleEntityAsync(Guid entityId) =>
        await Context.ProductManufacturers.SingleOrDefaultAsync
            (p => p.Id == entityId); 
}