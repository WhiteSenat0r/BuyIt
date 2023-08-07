using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class ProductTypeRepository : GenericRepository<ProductType>
{
    public ProductTypeRepository
        (StoreContext dbContext) : base(dbContext)
    {
        Context = dbContext;
        AllEntitiesAsync = EF.CompileAsyncQuery // Assigning delegates with values
                                                // depending on current repository type
            ((StoreContext context) => context.ProductTypes.ToList());
        SingleEntityAsync = EF.CompileAsyncQuery
            ((StoreContext context, Guid id) => context.ProductTypes.Single
                (p => p.Id == id));
    }
}