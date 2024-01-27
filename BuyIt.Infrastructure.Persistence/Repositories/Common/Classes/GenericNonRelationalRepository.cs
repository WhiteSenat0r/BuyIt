using System.Text.Json;
using Domain.Contracts.ProductListRelated;
using Domain.Contracts.RepositoryRelated.NonRelational;
using StackExchange.Redis;

namespace Persistence.Repositories.Common.Classes;

public abstract class GenericNonRelationalRepository<TItem, TEntity> : INonRelationalRepository<TEntity>
    where TItem : class, IProductListItem
    where TEntity : class, IProductList<TItem>, new()
{
    private readonly IDatabase _database;

    public GenericNonRelationalRepository(
        IConnectionMultiplexer multiplexer) => _database = multiplexer.GetDatabase();

    public async Task<TEntity> GetSingleEntityByIdAsync(Guid entityId)
    {
        var data = await _database.StringGetAsync(entityId.ToString());
        
        return data.IsNullOrEmpty 
            ? new TEntity { Id = entityId } 
            : JsonSerializer.Deserialize<TEntity>(data);
    }

    public async Task<TEntity> CreateOrUpdateEntityAsync(
        TEntity updatedEntity, int? daysToStoreData = null)
    {
        bool createdEntityResult;
        
        if (IsValidDataStoreValue(daysToStoreData))
            createdEntityResult = await _database.StringSetAsync(updatedEntity.Id.ToString(),
                JsonSerializer.Serialize(updatedEntity),
                TimeSpan.FromDays((int)daysToStoreData!));
        else
            createdEntityResult = await _database.StringSetAsync(updatedEntity.Id.ToString(),
                JsonSerializer.Serialize(updatedEntity));
        
        return GetUpdatedEntity(updatedEntity, createdEntityResult);
    }

    private bool IsValidDataStoreValue(int? daysToStoreData) => daysToStoreData is > 0;

    public async Task<bool> RemoveExistingEntityAsync(Guid removedEntityId) => 
        await _database.KeyDeleteAsync(removedEntityId.ToString());
    
    private TEntity GetUpdatedEntity(TEntity updatedEntity, bool createdEntityResult) => 
        !createdEntityResult ? new TEntity { Id = updatedEntity.Id } : updatedEntity;
}