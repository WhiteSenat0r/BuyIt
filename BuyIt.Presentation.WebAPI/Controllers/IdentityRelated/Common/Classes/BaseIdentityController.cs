using Application.DataTransferObjects.IdentityRelated;
using BuyIt.Presentation.WebAPI.Controllers.Common.Classes;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Contracts.TokenRelated;
using Domain.Entities.IdentityRelated;
using Microsoft.AspNetCore.Identity;

namespace BuyIt.Presentation.WebAPI.Controllers.IdentityRelated.Common.Classes;

public abstract class BaseIdentityRelatedController : BaseApiController
{
    private protected readonly UserManager<User> UserManager;
    private readonly IAuthenticationTokenService _authenticationTokenService;
    private protected readonly IRepository<RefreshToken> RefreshTokenRepository;

    protected BaseIdentityRelatedController(
        UserManager<User> userManager,
        IAuthenticationTokenService authenticationTokenService,
        IRepository<RefreshToken> refreshTokenRepository)
    {
        UserManager = userManager;
        _authenticationTokenService = authenticationTokenService;
        RefreshTokenRepository = refreshTokenRepository;
    }

    protected void RemoveTokenCookies()
    {
        Response.Cookies.Delete("UserAccessToken");
        Response.Cookies.Delete("UserRefreshToken");
    }
    
    protected async Task<UserDto> GetUserDataResponse(User user) =>
        new()
        {
            DisplayedName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Roles = await UserManager.GetRolesAsync(user),
            BasketId = user.BasketId,
            WishListId = user.WishListId,
            ComparisonListId = user.ComparisonListId
        };

    protected async Task SetRefreshTokenAsync(User user)
    {
        var refreshToken = _authenticationTokenService.CreateRefreshToken();
        refreshToken.UserId = user.Id;
        await RefreshTokenRepository.AddNewEntityAsync(refreshToken);
        
        user.RefreshTokens.Add(refreshToken);
        
        await UserManager.UpdateAsync(user);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = refreshToken.ExpiryDate
        };
        
        Response.Cookies.Append(
            "UserRefreshToken", refreshToken.Value, cookieOptions);
    }

    protected async Task GenerateAccessTokenAsync(User user)
    {
        var accessToken = _authenticationTokenService.CreateToken(user);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddMinutes(5)
        };
        
        Response.Cookies.Append(
            "UserAccessToken", accessToken, cookieOptions);
    }
}