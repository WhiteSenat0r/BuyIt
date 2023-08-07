using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.Factories.Common.Classes;

namespace Infrastructure.Repositories.Factories;

public class ProductManufacturerRepositoryFactory : RepositoryFactory<ProductManufacturer>
{
    public override IRepository<ProductManufacturer> Create(StoreContext dbContext) => 
        new ProductManufacturerRepository(dbContext);
}