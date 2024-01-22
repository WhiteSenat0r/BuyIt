using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.ProductRelatedRepositories;

public sealed class ProductSpecificationAttributeRepository : GenericRepository<ProductSpecificationAttribute>
{
    internal ProductSpecificationAttributeRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}