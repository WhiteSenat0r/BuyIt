using Domain.Entities.IdentityRelated;

namespace Domain.Contracts.TokenRelated;

public interface ITokenService
{
    string CreateAccessToken(User user);
    
    RefreshToken CreateRefreshToken();
}