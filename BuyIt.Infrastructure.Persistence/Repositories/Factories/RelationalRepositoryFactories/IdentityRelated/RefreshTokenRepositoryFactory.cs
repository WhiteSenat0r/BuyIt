using Domain.Entities.IdentityRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.IdentityRelatedRepositories;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.IdentityRelated;

internal sealed class RefreshTokenRepositoryFactory : RepositoryFactory<RefreshToken>
{
    internal override RefreshTokenRepository Create(StoreContext dbContext) => new(dbContext);
}