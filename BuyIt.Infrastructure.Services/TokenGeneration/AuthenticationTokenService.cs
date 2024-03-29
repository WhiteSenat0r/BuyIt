﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Contracts.TokenRelated;
using Domain.Entities.IdentityRelated;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BuyIt.Infrastructure.Services.TokenGeneration;

public sealed class AuthenticationTokenService : IAuthenticationTokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _symmetricSecurityKey;

    public AuthenticationTokenService(IConfiguration configuration)
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
    
    public RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var randomNumberGenerator = RandomNumberGenerator.Create();
        
        randomNumberGenerator.GetBytes(randomNumber);

        return new RefreshToken
        {
            Value = Convert.ToBase64String(randomNumber)
        };
    }

    private SecurityTokenDescriptor GetTokenDescriptor(
        IEnumerable<Claim> userClaims, SigningCredentials credentials) =>
        new()
        {
            Subject = new ClaimsIdentity(userClaims),
            Expires = DateTime.UtcNow.AddMinutes(5),
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