namespace Domain.Contracts.RepositoryRelated.NonRelational;

public interface INonRelationalRepository<TEntity> 
    where TEntity : class
{
    Task<TEntity> GetSingleEntityByIdAsync(Guid entityId);

    Task<TEntity> UpdateExistingEntityAsync(TEntity updatedEntity, int? daysToStoreData);
    
    Task<bool> RemoveExistingEntityAsync(Guid removedEntityId);
}