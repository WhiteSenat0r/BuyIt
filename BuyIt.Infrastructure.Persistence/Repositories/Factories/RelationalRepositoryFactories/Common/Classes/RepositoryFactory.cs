using Domain.Contracts.Common;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;

internal abstract class RepositoryFactory<TEntity> 
    where TEntity : class, IEntity<Guid>
{
    internal abstract GenericRepository<TEntity> Create(StoreContext dbContext);
}