using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories.ProductRelated;

public class ProductRepository : GenericRepository<Product>
{
    internal ProductRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;

    public override async void RemoveExistingEntity(Product removedEntity)
    {
        new ProductRatingRepository(Context).RemoveExistingEntity(removedEntity.Rating);

        var productsWithIdenticalManufacturer = await new ProductRepository(Context)
            .GetAllEntitiesAsync(new ProductQueryByManufacturerIdSpecification
                (removedEntity.ManufacturerId));
        
        if (productsWithIdenticalManufacturer.IsNullOrEmpty())
            new ProductManufacturerRepository(Context).RemoveExistingEntity
                (await new ProductManufacturerRepository(Context).GetSingleEntityBySpecificationAsync
                    (new ProductManufacturerQueryByIdSpecification(removedEntity.ManufacturerId)));
    }

    public override void RemoveRangeOfExistingEntities(IEnumerable<Product> removedEntities)
    {
        foreach (var removedEntity in removedEntities)
            RemoveExistingEntity(removedEntity);
    }
}