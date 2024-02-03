using System.Text;
using Domain.Contracts.TokenRelated;
using Domain.Entities.IdentityRelated;

namespace BuyIt.Infrastructure.Services.TokenGeneration;

public sealed class ConfirmationTokenService : IConfirmationTokenService
{
    public string CreateToken(User user)
    {
        var tokenParts = new List<string>
        {
            Convert.ToBase64String(user.Id.ToByteArray()),
            Convert.ToBase64String(Encoding.UTF8.GetBytes(user.UserName!)),
            Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Email!))
        };

        return tokenParts.Aggregate(
            (currentElement, nextElement) => currentElement + "$" + nextElement)
            .Replace('+', '*');
    }

    public bool IsValidConfirmationToken(User user, string token) => 
        CreateToken(user).Equals(token);
}