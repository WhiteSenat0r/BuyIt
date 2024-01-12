using Domain.Contracts.ProductListRelated;
using Persistence.Repositories.Common.Classes;
using StackExchange.Redis;

namespace Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Common.Classes;

public abstract class NonRelationalRepositoryFactory<TEntity> 
    where TEntity : class, IProductList<IProductListItem>, new()
{
    public abstract GenericNonRelationalRepository<TEntity> Create(
        IConnectionMultiplexer connectionMultiplexer);
}