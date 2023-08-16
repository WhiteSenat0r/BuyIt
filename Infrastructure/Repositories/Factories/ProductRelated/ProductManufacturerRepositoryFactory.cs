using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Factories.Common.Classes;
using Infrastructure.Repositories.ProductRelated;

namespace Infrastructure.Repositories.Factories.ProductRelated;

public class ProductManufacturerRepositoryFactory : RepositoryFactory<ProductManufacturer>
{
    public override ProductManufacturerRepository Create(StoreContext dbContext) => new(dbContext);
}