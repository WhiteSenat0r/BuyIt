using System.Linq.Expressions;
using Core.Entities.Product;
using Core.Entities.Product.Common.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>
{
    internal ProductRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;

    public override async Task<IEnumerable<Product>> GetAllEntitiesAsync() =>
        await Context.Products
            .Include(t => t.ProductType)
            .Include(m => m.Manufacturer)
            .Include(r => r.Rating)
            .ToListAsync();

    public override async Task<IEnumerable<Product>> GetEntitiesByFilterAsync
        (Expression<Func<Product, bool>> filter) => 
        await Context.Products
            .Include(t => t.ProductType)
            .Include(m => m.Manufacturer)
            .Include(r => r.Rating)
            .Where(filter).ToListAsync();

    public override async Task<Product> GetSingleEntityAsync(Guid entityId) => 
        await Context.Products
            .Include(t => t.ProductType)
            .Include(m => m.Manufacturer)
            .Include(r => r.Rating)
            .SingleAsync();
    
    public override void RemoveExistingEntity(Product removedEntity)
    {
        void RemoveEntity(IProduct product) =>
            new ProductRatingRepository(Context).RemoveExistingEntity(product.Rating);
        RemoveEntity(removedEntity);
    }
}