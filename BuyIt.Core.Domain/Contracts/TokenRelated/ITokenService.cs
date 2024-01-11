using Domain.Entities;

namespace Domain.Contracts.TokenRelated;

public interface ITokenService
{
    string CreateToken(User user);
}