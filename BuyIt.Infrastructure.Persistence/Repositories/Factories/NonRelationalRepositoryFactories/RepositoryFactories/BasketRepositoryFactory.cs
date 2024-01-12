using Domain.Contracts.RepositoryRelated.NonRelational;
using Domain.Entities.ProductListRelated;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Repositories;
using StackExchange.Redis;

namespace Persistence.Repositories.Factories.NonRelationalRepositoryFactories.RepositoryFactories;

public class BasketRepositoryFactory : NonRelationalRepositoryFactory<ProductList<BasketItem>>
{
    public override INonRelationalRepository<ProductList<BasketItem>> Create(
        IConnectionMultiplexer connectionMultiplexer) =>
        new BasketRepository(connectionMultiplexer);
}