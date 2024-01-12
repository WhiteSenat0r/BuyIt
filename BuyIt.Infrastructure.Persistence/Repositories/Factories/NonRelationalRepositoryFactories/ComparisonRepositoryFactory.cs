using Domain.Entities.ProductListRelated;
using Persistence.Repositories.Common.Classes;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Common.Classes;
using StackExchange.Redis;

namespace Persistence.Repositories.Factories.NonRelationalRepositoryFactories;

public class ComparisonRepositoryFactory : NonRelationalRepositoryFactory<ProductList<ComparedItem>>
{
    public override GenericNonRelationalRepository<ProductList<ComparedItem>> Create(
        IConnectionMultiplexer connectionMultiplexer) => new(connectionMultiplexer);
}