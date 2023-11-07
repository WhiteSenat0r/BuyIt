using Domain.Contracts.Common;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.Factories.Common.Classes;

public abstract class RepositoryFactory<TEntity> 
    where TEntity : class, IEntity<Guid>
{
    public abstract GenericRepository<TEntity> Create(StoreContext dbContext);
}