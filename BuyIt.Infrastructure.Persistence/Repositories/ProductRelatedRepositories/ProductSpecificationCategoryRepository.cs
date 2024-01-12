using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.ProductRelatedRepositories;

public sealed class ProductSpecificationCategoryRepository : GenericRepository<ProductSpecificationCategory>
{
    internal ProductSpecificationCategoryRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}