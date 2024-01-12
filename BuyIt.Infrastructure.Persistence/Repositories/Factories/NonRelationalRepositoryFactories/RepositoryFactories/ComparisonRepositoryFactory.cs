using Domain.Contracts.RepositoryRelated.NonRelational;
using Domain.Entities.ProductListRelated;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Repositories;
using StackExchange.Redis;

namespace Persistence.Repositories.Factories.NonRelationalRepositoryFactories.RepositoryFactories;

public class ComparisonRepositoryFactory : NonRelationalRepositoryFactory<ProductList<ComparedItem>>
{
    public override INonRelationalRepository<ProductList<ComparedItem>> Create(
        IConnectionMultiplexer connectionMultiplexer) =>
        new ComparisonRepository(connectionMultiplexer);
}