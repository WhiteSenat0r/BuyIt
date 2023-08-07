using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;

namespace Infrastructure.Repositories.Common.Classes;

public abstract class GenericRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected GenericRepository(StoreContext dbContext) => 
        Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    private protected StoreContext Context { get; init; }
    
    private protected Func<StoreContext, Task<List<TEntity>>> // Func delegate that is being used for invocation
                                                              // in GetAllEntitiesAsync method as a compiled
                                                              // query which returns all entities from the database 
        AllEntitiesAsync { get; init; } = null!;

    private protected Func<StoreContext, Guid, Task<TEntity>> // Func delegate that is being used for invocation
                                                              // in GetSingleEntityAsync method as a compiled
                                                              // query which returns a single entity from the database
        SingleEntityAsync { get; init; } = null!;

    public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync() => 
        await AllEntitiesAsync(Context);
    
    public async Task<TEntity> GetSingleEntityAsync(Guid id) => 
        await SingleEntityAsync(Context, id);
    
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
