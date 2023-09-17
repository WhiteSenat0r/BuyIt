using Core.Entities.Product.ProductSpecification;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.ProductSpecificationRelated;

public sealed class ProductSpecificationCategoryRepository : GenericRepository<ProductSpecificationCategory>
{
    internal ProductSpecificationCategoryRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}