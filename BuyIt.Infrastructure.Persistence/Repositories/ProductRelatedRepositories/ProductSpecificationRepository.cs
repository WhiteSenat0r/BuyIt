using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.ProductRelatedRepositories;

public sealed class ProductSpecificationRepository : GenericRepository<ProductSpecification>
{
    internal ProductSpecificationRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}