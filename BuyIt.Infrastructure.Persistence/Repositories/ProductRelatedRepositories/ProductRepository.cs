using Domain.Contracts.ProductRelated;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.ProductRelatedRepositories;

public sealed class ProductRepository : GenericRepository<Product>
{
    internal ProductRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;

    public override void RemoveExistingEntity(Product removedEntity)
    {
        RemoveOrphanedManufacturer(removedEntity);
        
        foreach (var specification in removedEntity.Specifications)
        {
            RemoveOrphanedSpecificationCategory(specification.SpecificationCategoryId);
            RemoveOrphanedSpecificationAttribute(specification.SpecificationAttributeId);
            RemoveOrphanedSpecificationValue(specification.SpecificationValueId);
        }
        
        Context.ProductSpecifications.RemoveRange(removedEntity.Specifications);
        Context.ProductRatings.Remove(Context.ProductRatings.Single(r => r.Id == removedEntity.RatingId));

        Context.SaveChanges();
    }

    private void RemoveOrphanedSpecificationCategory(Guid categoryId)
    {
        if (Context.ProductSpecifications.Count(s => s.SpecificationCategoryId == categoryId) == 1)
            Context.ProductSpecificationCategories.Remove(
                Context.ProductSpecificationCategories.Single(c => c.Id == categoryId));
    }

    private void RemoveOrphanedSpecificationAttribute(Guid attributeId)
    {
        if (Context.ProductSpecifications.Count(s => s.SpecificationAttributeId == attributeId) == 1)
            Context.ProductSpecificationAttributes.Remove(
                Context.ProductSpecificationAttributes.Single(a => a.Id == attributeId));
    }

    private void RemoveOrphanedSpecificationValue(Guid valueId)
    {
        if (Context.ProductSpecifications.Count(s => s.SpecificationValueId == valueId) == 1)
            Context.ProductSpecificationValues.Remove(
                Context.ProductSpecificationValues.Single(v => v.Id == valueId));
    }
    
    private void RemoveOrphanedManufacturer(IProduct removedEntity)
    {
        if (Context.Products.Count(p => p.ManufacturerId == removedEntity.ManufacturerId) == 1)
            Context.ProductManufacturers.Remove
                (Context.ProductManufacturers.Single(m => m.Id == removedEntity.ManufacturerId));
    }

    public override void RemoveRangeOfExistingEntities(IEnumerable<Product> removedEntities)
    {
        foreach (var removedEntity in removedEntities)
            RemoveExistingEntity(removedEntity);
    }
}