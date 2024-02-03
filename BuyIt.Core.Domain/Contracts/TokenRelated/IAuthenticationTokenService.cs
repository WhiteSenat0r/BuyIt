using Domain.Entities.IdentityRelated;

namespace Domain.Contracts.TokenRelated;

public interface IAuthenticationTokenService : ITokenService
{
    RefreshToken CreateRefreshToken();
}