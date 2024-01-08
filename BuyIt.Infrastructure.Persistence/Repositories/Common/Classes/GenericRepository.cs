using Application.Specifications.Common;
using Domain.Contracts.Common;
using Domain.Contracts.RepositoryRelated.Relational;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories.Common.Classes;

public abstract class GenericRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity<Guid>
{
    protected GenericRepository(StoreContext dbContext) => 
        Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    private protected StoreContext Context { get; init; }

    public Task<List<TEntity>> GetAllEntitiesAsync
        (IQuerySpecification<TEntity> querySpecification) =>
        ApplySpecification(querySpecification).ToListAsync();

    public Task<TEntity> GetSingleEntityBySpecificationAsync
        (IQuerySpecification<TEntity> querySpecification) =>
        ApplySpecification(querySpecification).SingleOrDefaultAsync()!;

    public async Task AddNewEntityAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task AddNewRangeOfEntitiesAsync(IEnumerable<TEntity> entities)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities);
        await Context.SaveChangesAsync();
    }

    public void UpdateExistingEntity(TEntity updatedEntity)
    {
        Context.Set<TEntity>().Update(updatedEntity);
        Context.SaveChanges();
    }

    public void UpdateRangeOfExistingEntities(IEnumerable<TEntity> updatedEntities)
    {
        Context.Set<TEntity>().UpdateRange(updatedEntities);
        Context.SaveChanges();
    }

    public virtual void RemoveExistingEntity(TEntity removedEntity)
    {
        Context.Set<TEntity>().Remove(removedEntity);
        Context.SaveChanges();
    }

    public virtual void RemoveRangeOfExistingEntities(IEnumerable<TEntity> removedEntities)
    {
        Context.Set<TEntity>().RemoveRange(removedEntities);
        Context.SaveChanges();
    }

    public Task<int> CountAsync(IQuerySpecification<TEntity> querySpecification) => 
        Context.Set<TEntity>().Where(querySpecification.Criteria).CountAsync();

    private IQueryable<TEntity> ApplySpecification(IQuerySpecification<TEntity> querySpecification) =>
        QuerySpecificationEvaluator.GetQuerySpecifications(Context.Set<TEntity>(), querySpecification);
}
