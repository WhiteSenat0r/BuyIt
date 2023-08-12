using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductTypeRepository : GenericRepository<ProductType>
{
    internal ProductTypeRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
    
    public override async Task<ProductType?> GetSingleEntityAsync(Guid entityId) =>
        await Context.ProductTypes.SingleOrDefaultAsync(p => p.Id == entityId);
}