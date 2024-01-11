using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Contracts.TokenRelated;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BuyIt.Infrastructure.Services.TokenGeneration;

public sealed class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _symmetricSecurityKey;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Token:Key"]!));
    }
    
    public string CreateToken(User user)
    {
        var userClaims = GetUserClaims(user);

        var credentials = GetSigningCredentials();

        var tokenDescriptor = GetTokenDescriptor(userClaims, credentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(jwtToken);
    }

    private SecurityTokenDescriptor GetTokenDescriptor(
        IEnumerable<Claim> userClaims, SigningCredentials credentials) =>
        new()
        {
            Subject = new ClaimsIdentity(userClaims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _configuration["Token:Issuer"]
        };

    private SigningCredentials GetSigningCredentials() =>
        new(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

    private IEnumerable<Claim> GetUserClaims(User user) =>
        new List<Claim>
        {
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.UserName!)
        };
}