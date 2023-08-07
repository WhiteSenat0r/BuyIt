#nullable enable
namespace Infrastructure.Repositories.Common.Interfaces;

public interface IRepository<TEntity> 
    where TEntity : class
{
    Task<IEnumerable<TEntity>?> GetAllEntitiesAsync();
    
    Task<TEntity?> GetSingleEntityAsync(Guid id);

    Task AddNewEntityAsync(TEntity entity);
    
    void UpdateExistingEntity(TEntity updatedEntity);
    
    void RemoveExistingEntity(TEntity removedEntity);
}