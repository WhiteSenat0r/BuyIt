using System.Linq.Expressions;

namespace Infrastructure.Repositories.Common.Interfaces;

public interface IRepository<TEntity> 
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllEntitiesAsync();

    Task<IEnumerable<TEntity>> GetEntitiesByFilterAsync
        (Expression<Func<TEntity, bool>> filter);

    Task<TEntity?> GetSingleEntityAsync(Guid entityId);
    
    Task AddNewEntityAsync(TEntity entity);
    
    Task AddNewRangeOfEntitiesAsync(IEnumerable<TEntity> entities);
    
    void UpdateExistingEntity(TEntity updatedEntity);
    
    void UpdateRangeOfExistingEntities(IEnumerable<TEntity> updatedEntities);
    
    void RemoveExistingEntity(TEntity removedEntity);
    
    void RemoveRangeOfExistingEntities(IEnumerable<TEntity> removedEntities);
}