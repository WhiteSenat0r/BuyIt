using Domain.Entities.IdentityRelated;

namespace Domain.Contracts.TokenRelated;

public interface IConfirmationTokenService : ITokenService
{
    bool IsValidConfirmationToken(User user, string token);
}