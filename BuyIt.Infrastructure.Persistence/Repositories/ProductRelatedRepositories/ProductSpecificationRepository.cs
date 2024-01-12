using Domain.Contracts.ProductRelated;
using Domain.Entities.ProductRelated;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.ProductRelatedRepositories;

public sealed class ProductSpecificationRepository : GenericRepository<ProductSpecification>
{
    internal ProductSpecificationRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;

    public async Task TryAddNewSpecificationAsync(string specCategoryName,
        string specAttributeName, string specValueName)
    {
        var statuses = await GetSpecificationRelatedStatuses(
            specCategoryName, specAttributeName, specValueName);

        if (statuses["Specification"]) return;
        
        var mappedValues = GetMappedIncomeParameters(
            specCategoryName, specAttributeName, specValueName);

        var aspects = InstantiateMissingAspects(mappedValues, statuses);

        await AddCreatedAspectsToDatabaseAsync(aspects);

        var newSpecification = await InstantiateNewSpecificationAsync(
            specCategoryName, specAttributeName, specValueName);

        await AddNewSpecificationToDatabaseAsync(newSpecification);
    }

    private Dictionary<string, string> GetMappedIncomeParameters(
        string specCategoryName, string specAttributeName, string specValueName) =>
        new()
        {
            { "Category", specCategoryName },
            { "Attribute", specAttributeName },
            { "Value", specValueName },
        };

    private async Task AddNewSpecificationToDatabaseAsync(ProductSpecification newSpecification)
    {
        await Context.ProductSpecifications.AddAsync(newSpecification);
        await Context.SaveChangesAsync();
    }

    private async Task<ProductSpecification> InstantiateNewSpecificationAsync(
        string specCategoryName, string specAttributeName, string specValueName) =>
        new()
        {
            SpecificationCategoryId = await GetAspectIdAsync(
                Context.ProductSpecificationCategories, specCategoryName),
            SpecificationAttributeId = await GetAspectIdAsync(
                Context.ProductSpecificationAttributes, specAttributeName),
            SpecificationValueId = await GetAspectIdAsync(
                Context.ProductSpecificationValues, specValueName)
        };

    private async Task<Guid> GetAspectIdAsync<TEntity>(
        IQueryable<TEntity> entities, string specAspectName)
        where TEntity : class, ISpecificationAspect =>
        (await entities.SingleAsync(c => c.Value.Equals(specAspectName))).Id;

    private async Task AddCreatedAspectsToDatabaseAsync(
        Dictionary<string, ISpecificationAspect> aspects)
    {
        foreach (var aspect in aspects)
        {
            await AddAspectToParticularSetAsync(aspect);
            await Context.SaveChangesAsync();
        }
    }

    private async Task AddAspectToParticularSetAsync(
        KeyValuePair<string, ISpecificationAspect> aspect)
    {
        switch (aspect.Key)
        {
            case "Category": 
                await AddNewAspectToDatabaseAsync(
                    Context.ProductSpecificationCategories, aspect.Value);
                break;
            case "Attribute": 
                await AddNewAspectToDatabaseAsync(
                    Context.ProductSpecificationAttributes, aspect.Value);
                break;
            case "Value": 
                await AddNewAspectToDatabaseAsync(
                    Context.ProductSpecificationValues, aspect.Value);
                break;
        }
    }

    private async Task AddNewAspectToDatabaseAsync<TEntityType>(
        DbSet<TEntityType> entities, ISpecificationAspect aspect)
        where TEntityType : class,ISpecificationAspect =>
        await entities.AddAsync((TEntityType)aspect);

    private Dictionary<string, ISpecificationAspect> InstantiateMissingAspects(
        IReadOnlyDictionary<string, string> mappedValues,
        IReadOnlyDictionary<string, bool> statuses) =>
        statuses.Take(3).Where(
            status => !statuses[status.Key]).ToDictionary(
            status => status.Key,
            status => GetSuitableAspect(
                status.Key, mappedValues[status.Key]));

    private ISpecificationAspect GetSuitableAspect(
        string aspectDeterminant, string aspectValue) =>
        aspectDeterminant switch
        {
            "Category" => new ProductSpecificationCategory(aspectValue),
            "Attribute" => new ProductSpecificationAttribute(aspectValue),
            "Value" => new ProductSpecificationValue(aspectValue),
        };

    private async Task<Dictionary<string, bool>> GetSpecificationRelatedStatuses(
        string specCategoryName, string specAttributeName, string specValueName) =>
        new()
        {
            { "Category", await IsExistingAspect(
                Context.ProductSpecificationCategories, specCategoryName) },
            { "Attribute", await IsExistingAspect(
                Context.ProductSpecificationAttributes, specAttributeName) },
            { "Value", await IsExistingAspect(
                Context.ProductSpecificationValues, specValueName) },
            { "Specification", await IsExistingSpecification(
                specCategoryName, specAttributeName, specValueName) }
        };

    private async Task<bool> IsExistingSpecification(string specCategoryName,
        string specAttributeName, string specValueName) =>
        await Context.ProductSpecifications.SingleOrDefaultAsync(
            s => s.SpecificationCategory.Value.Equals(specCategoryName) &&
                 s.SpecificationAttribute.Value.Equals(specAttributeName) &&
                 s.SpecificationValue.Value.Equals(specValueName)) is not null;
    
    private async Task<bool> IsExistingAspect<TEntity>(
        IQueryable<TEntity> entities, string specAspectName)
        where TEntity : class, ISpecificationAspect =>
        await entities.SingleOrDefaultAsync(
            c => c.Value.Equals(specAspectName)) is not null;
}