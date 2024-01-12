using Domain.Entities.ProductListRelated;
using Persistence.Repositories.Common.Classes;
using StackExchange.Redis;

namespace Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Repositories;

internal class BasketRepository : GenericNonRelationalRepository<BasketItem, ProductList<BasketItem>>
{
    internal BasketRepository(IConnectionMultiplexer multiplexer) : base(multiplexer) { }
}