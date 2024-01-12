using Domain.Contracts.Common;
using Domain.Contracts.RepositoryRelated.NonRelational;
using StackExchange.Redis;

namespace Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Common.Classes;

public abstract class NonRelationalRepositoryFactory<TEntity>
    where TEntity : class, IEntity<Guid>
{
    public abstract INonRelationalRepository<TEntity> Create(
        IConnectionMultiplexer connectionMultiplexer);
}