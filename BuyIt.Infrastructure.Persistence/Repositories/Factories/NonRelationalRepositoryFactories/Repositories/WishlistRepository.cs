using Domain.Entities.ProductListRelated;
using Persistence.Repositories.Common.Classes;
using StackExchange.Redis;

namespace Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Repositories;

internal class WishlistRepository : GenericNonRelationalRepository<WishedItem, ProductList<WishedItem>>
{
    internal WishlistRepository(IConnectionMultiplexer multiplexer) : base(multiplexer) { }
}