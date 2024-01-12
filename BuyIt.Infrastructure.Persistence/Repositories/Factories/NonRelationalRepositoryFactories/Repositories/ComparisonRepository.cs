using Domain.Entities.ProductListRelated;
using Persistence.Repositories.Common.Classes;
using StackExchange.Redis;

namespace Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Repositories;

internal class ComparisonRepository : GenericNonRelationalRepository<ComparedItem, ProductList<ComparedItem>>
{
    internal ComparisonRepository(IConnectionMultiplexer multiplexer) : base(multiplexer) { }
}