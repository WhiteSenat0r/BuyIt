using Domain.Entities.IdentityRelated;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.IdentityRelatedRepositories;

public sealed class RefreshTokenRepository : GenericRepository<RefreshToken>
{
    internal RefreshTokenRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}