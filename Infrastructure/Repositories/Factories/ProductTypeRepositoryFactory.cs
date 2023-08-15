using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Factories.Common.Classes;

namespace Infrastructure.Repositories.Factories;

public class ProductTypeRepositoryFactory : RepositoryFactory<ProductType>
{
    public override ProductTypeRepository Create(StoreContext dbContext) =>  new(dbContext);
}