using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories;

public class ProductTypeRepository : GenericRepository<ProductType>
{
    internal ProductTypeRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}