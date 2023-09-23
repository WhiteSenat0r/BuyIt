using Core.Entities.Product.ProductSpecificationRelated;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Factories.Common.Classes;
using Infrastructure.Repositories.ProductRelated.ProductSpecificationRelated;

namespace Infrastructure.Repositories.Factories.ProductRelated;

public class ProductSpecificationAttributeRepositoryFactory : RepositoryFactory<ProductSpecificationAttribute>
{
    public override ProductSpecificationAttributeRepository Create(StoreContext dbContext) =>  new(dbContext);
}