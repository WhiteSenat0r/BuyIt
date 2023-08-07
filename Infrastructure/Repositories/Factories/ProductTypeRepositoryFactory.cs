using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.Factories.Common.Classes;

namespace Infrastructure.Repositories.Factories;

public class ProductTypeRepositoryFactory : RepositoryFactory<ProductType>
{
    public override IRepository<ProductType> Create(StoreContext dbContext) => 
        new ProductTypeRepository(dbContext);
}