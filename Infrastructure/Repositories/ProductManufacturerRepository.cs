using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class ProductManufacturerRepository : GenericRepository<ProductManufacturer>
{
    public ProductManufacturerRepository
        (StoreContext dbContext) : base(dbContext)
    {
        Context = dbContext;
        AllEntitiesAsync = EF.CompileAsyncQuery // Assigning delegates with values
                                                // depending on current repository type
            ((StoreContext context) => context.ProductManufacturers.ToList());
        SingleEntityAsync = EF.CompileAsyncQuery
            ((StoreContext context, Guid id) => context.ProductManufacturers.Single
                (p => p.Id == id));
    }
}