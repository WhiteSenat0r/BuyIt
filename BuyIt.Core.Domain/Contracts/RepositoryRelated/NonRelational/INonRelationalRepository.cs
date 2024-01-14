namespace Domain.Contracts.RepositoryRelated.NonRelational;

public interface INonRelationalRepository<TEntity> 
    where TEntity : class
{
    Task<TEntity> GetSingleEntityByIdAsync(Guid entityId);

    Task<TEntity> CreateOrUpdateEntityAsync(TEntity updatedEntity, int? daysToStoreData = null);
    
    Task<bool> RemoveExistingEntityAsync(Guid removedEntityId);
}