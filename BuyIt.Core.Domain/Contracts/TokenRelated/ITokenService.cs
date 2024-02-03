using Domain.Entities.IdentityRelated;

namespace Domain.Contracts.TokenRelated;

public interface ITokenService
{
    string CreateToken(User user);
}