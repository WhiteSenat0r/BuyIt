using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Common.Classes;

public abstract class GenericRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected GenericRepository(StoreContext dbContext) => 
        Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    private protected StoreContext Context { get; init; }
    
    public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync() => 
        await Context.Set<TEntity>().ToListAsync();

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

    public void RemoveExistingEntity(TEntity removedEntity)
    {
        Context.Set<TEntity>().Remove(removedEntity);
        Context.SaveChanges();
    }

    public void RemoveRangeOfExistingEntities(IEnumerable<TEntity> removedEntities)
    {
        Context.Set<TEntity>().RemoveRange(removedEntities);
        Context.SaveChanges();
    }
}
