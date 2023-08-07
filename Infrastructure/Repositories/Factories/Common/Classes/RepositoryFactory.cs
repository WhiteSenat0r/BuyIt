using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;

namespace Infrastructure.Repositories.Factories.Common.Classes;

public abstract class RepositoryFactory<TEntity> 
    where TEntity : class
{
    public abstract IRepository<TEntity> Create(StoreContext dbContext);
}