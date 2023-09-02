using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated;

public sealed class ProductSpecificationRepository : GenericRepository<ProductSpecification>
{
    internal ProductSpecificationRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}