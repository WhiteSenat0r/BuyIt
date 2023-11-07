using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.ProductRelatedRepositories;

public sealed class ProductManufacturerRepository : GenericRepository<ProductManufacturer>
{
    internal ProductManufacturerRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}