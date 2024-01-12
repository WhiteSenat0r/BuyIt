using Domain.Contracts.RepositoryRelated.NonRelational;
using Domain.Entities.ProductListRelated;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Repositories;
using StackExchange.Redis;

namespace Persistence.Repositories.Factories.NonRelationalRepositoryFactories.RepositoryFactories;

public class WishlistRepositoryFactory : NonRelationalRepositoryFactory<ProductList<WishedItem>>
{
    public override INonRelationalRepository<ProductList<WishedItem>> Create(
        IConnectionMultiplexer connectionMultiplexer) =>
        new WishlistRepository(connectionMultiplexer);
}