using Domain.Entities.ProductListRelated;
using Persistence.Repositories.Common.Classes;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Common.Classes;
using StackExchange.Redis;

namespace Persistence.Repositories.Factories.NonRelationalRepositoryFactories;

public class WishlistRepositoryFactory : NonRelationalRepositoryFactory<ProductList<WishedItem>>
{
    public override GenericNonRelationalRepository<ProductList<WishedItem>> Create(
        IConnectionMultiplexer connectionMultiplexer) => new(connectionMultiplexer);
}