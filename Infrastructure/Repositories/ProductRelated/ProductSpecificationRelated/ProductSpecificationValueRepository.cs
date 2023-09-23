using Core.Entities.Product.ProductSpecificationRelated;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.ProductSpecificationRelated;

public sealed class ProductSpecificationValueRepository : GenericRepository<ProductSpecificationValue>
{
    internal ProductSpecificationValueRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}