using Core.Entities.Product;
using Core.Entities.Product.Common.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated;

public class ProductRepository : GenericRepository<Product>
{
    internal ProductRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;

    public override void RemoveExistingEntity(Product removedEntity)
    {
        void RemoveEntity(IProduct product) =>
            new ProductRatingRepository(Context).RemoveExistingEntity(product.Rating);
        RemoveEntity(removedEntity);
    }
}