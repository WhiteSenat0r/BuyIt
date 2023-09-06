using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated;

public class ProductRepository : GenericRepository<Product>
{
    internal ProductRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;

    public override void RemoveExistingEntity(Product removedEntity)
    {
        if (Context.Products.Count(p => p.ManufacturerId == removedEntity.ManufacturerId) == 1)
            Context.ProductManufacturers.Remove
                (Context.ProductManufacturers.Single(m => m.Id == removedEntity.ManufacturerId));
        
        Context.ProductSpecifications.RemoveRange(removedEntity.Specifications);
        Context.ProductRatings.Remove(Context.ProductRatings.Single(r => r.Id == removedEntity.RatingId));

        Context.SaveChanges();
    }

    public override void RemoveRangeOfExistingEntities(IEnumerable<Product> removedEntities)
    {
        foreach (var removedEntity in removedEntities)
            RemoveExistingEntity(removedEntity);
    }
}