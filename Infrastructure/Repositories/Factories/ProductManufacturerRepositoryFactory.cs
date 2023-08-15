using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Factories.Common.Classes;

namespace Infrastructure.Repositories.Factories;

public class ProductManufacturerRepositoryFactory : RepositoryFactory<ProductManufacturer>
{
    public override ProductManufacturerRepository Create(StoreContext dbContext) => new(dbContext);
}