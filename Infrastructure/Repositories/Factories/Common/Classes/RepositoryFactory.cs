using Core.Common.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories.Factories.Common.Classes;

public abstract class RepositoryFactory<TEntity> 
    where TEntity : class, IEntity<Guid>
{
    public abstract GenericRepository<TEntity> Create(StoreContext dbContext);
}