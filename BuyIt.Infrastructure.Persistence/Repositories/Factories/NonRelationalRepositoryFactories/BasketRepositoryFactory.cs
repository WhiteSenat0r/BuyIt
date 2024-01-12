using Domain.Entities.ProductListRelated;
using Persistence.Repositories.Common.Classes;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Common.Classes;
using StackExchange.Redis;

namespace Persistence.Repositories.Factories.NonRelationalRepositoryFactories;

public class BasketRepositoryFactory : NonRelationalRepositoryFactory<ProductList<BasketItem>>
{
    public override GenericNonRelationalRepository<ProductList<BasketItem>> Create(
        IConnectionMultiplexer connectionMultiplexer) => new(connectionMultiplexer);
}