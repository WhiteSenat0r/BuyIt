using Core.Common.Interfaces;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Interfaces;

namespace Infrastructure.Repositories.Common.Interfaces;

public interface IRepository<TEntity> 
    where TEntity : class, IEntity<Guid>
{
    Task<List<TEntity>> GetAllEntitiesAsync
        (IQuerySpecification<TEntity> querySpecification);

    Task<TEntity> GetSingleEntityBySpecificationAsync
        (IQuerySpecification<TEntity> querySpecification);

    Task AddNewEntityAsync(TEntity entity);
    
    Task AddNewRangeOfEntitiesAsync(IEnumerable<TEntity> entities);
    
    void UpdateExistingEntity(TEntity updatedEntity);
    
    void UpdateRangeOfExistingEntities(IEnumerable<TEntity> updatedEntities);
    
    void RemoveExistingEntity(TEntity removedEntity);
    
    void RemoveRangeOfExistingEntities(IEnumerable<TEntity> removedEntities);

    int Count();

    int Count(Func<TEntity, bool> predicate);
}