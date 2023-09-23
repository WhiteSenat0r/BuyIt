using Core.Entities.Product.ProductSpecificationRelated;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.ProductSpecificationRelated;

public sealed class ProductSpecificationAttributeRepository : GenericRepository<ProductSpecificationAttribute>
{
    internal ProductSpecificationAttributeRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}