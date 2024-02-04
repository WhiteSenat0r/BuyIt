using Application.DataTransferObjects.IdentityRelated;
using BuyIt.Presentation.WebAPI.Controllers.IdentityRelated.Common.Classes;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Contracts.TokenRelated;
using Domain.Entities.IdentityRelated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BuyIt.Presentation.WebAPI.Controllers.IdentityRelated;

public class AuthenticationController : BaseIdentityRelatedController
{
    public AuthenticationController(UserManager<User> userManager,
        IAuthenticationTokenService authenticationTokenService,
        IRepository<RefreshToken> refreshTokenRepository) 
        : base(userManager, authenticationTokenService, refreshTokenRepository)
    { }

    [AllowAnonymous]
    [HttpGet("AuthStatus")]
    [ProducesResponseType(typeof(AuthStatusDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> GetCookiesStatus() =>
        Ok(new AuthStatusDto
        {
            ContainsAccessToken = !Request.Cookies["UserAccessToken"].IsNullOrEmpty(),
            ContainsRefreshToken = !Request.Cookies["UserRefreshToken"].IsNullOrEmpty(),
        });

    [AllowAnonymous]
    [HttpPut("RefreshAccessToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> RefreshAccessToken()
    {
        var refreshToken = Request.Cookies["UserRefreshToken"];

        var user = await UserManager.Users
            .Include(r => r.RefreshTokens)
            .SingleOrDefaultAsync(
                u => u.RefreshTokens.SingleOrDefault(
                    t => t.Value.Equals(refreshToken)) != null);

        if (user is null) return Unauthorized("User is not authorized!");

        var oldToken = user.RefreshTokens.SingleOrDefault(
            r => r.Value.Equals(refreshToken));

        if (oldToken is not null && !oldToken.IsValid)
        {
            RemoveTokenCookies();

            return Unauthorized("Refresh token is expired!");
        }

        if (oldToken is not null)
        {
            oldToken.RevocationDate = DateTime.UtcNow;

            RefreshTokenRepository.UpdateExistingEntity(oldToken);
        }

        var invalidUserTokens = user.RefreshTokens.Where(t => !t.IsValid);

        if (!invalidUserTokens.IsNullOrEmpty())
            RefreshTokenRepository.RemoveRangeOfExistingEntities(invalidUserTokens);

        await GenerateAccessTokenAsync(user);
        await SetRefreshTokenAsync(user);

        return Ok();
    }
}