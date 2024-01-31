using System.Globalization;
using System.Security.Claims;
using Application.DataTransferObjects.IdentityRelated;
using Application.Responses;
using Application.Responses.Common.Classes;
using BuyIt.Infrastructure.Services.Mailing;
using BuyIt.Infrastructure.Services.Mailing.Common.Items.MessageTemplates.MessageTemplates;
using BuyIt.Infrastructure.Services.Mailing.Common.Items.Options;
using BuyIt.Presentation.WebAPI.Controllers.Common.Classes;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Contracts.TokenRelated;
using Domain.Entities.IdentityRelated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BuyIt.Presentation.WebAPI.Controllers.IdentityRelated;

public class UserController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<UserRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly IRepository<RefreshToken> _refreshTokenRepository;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager,
        RoleManager<UserRole> roleManager, ITokenService tokenService,
        IRepository<RefreshToken> refreshTokenRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    [Authorize]
    [HttpGet("CurrentUser")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        var user = await _userManager.FindByEmailAsync(userEmail);
        
        return Ok(await GetUserDataResponse(user));
    }
    
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
    [HttpPost("Login")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto loginData)
    {
        if (IsInvalidModel(out var badRequest,
                "Error occured during login of a user!",
                new [] { "Email or password contains invalid value!" }))
            return badRequest;
        
        if (!await IsRegisteredEmail(loginData.Email)) 
            return BadRequest(new ApiResponse(
                400, "User with such email does not exist!"));

        var user = await _userManager.FindByEmailAsync(loginData.Email);
        
        if (!user.EmailConfirmed) return Unauthorized(
            new ApiResponse(401, "The email is not verified!"));
        
        var passwordIsValid = await _signInManager.CheckPasswordSignInAsync(
            user, loginData.Password, false);

        if (!passwordIsValid.Succeeded)
            return Unauthorized(new ApiResponse(401, "Incorrect password!"));

        await SetRefreshTokenAsync(user);
        await GenerateAccessTokenAsync(user);
        
        return Ok(await GetUserDataResponse(user));
    }
    
    [Authorize]
    [HttpPost("Logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Logout()
    {
        var user = await _userManager.Users.Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.UserName.Equals(
                User.FindFirstValue(ClaimTypes.Name)));

        if (user is null) return Unauthorized(
            new ApiResponse(401, "User is not authorized!"));

        var refreshToken = Request.Cookies["UserRefreshToken"];

        var oldToken = user.RefreshTokens.SingleOrDefault(t => t.Value.Equals(refreshToken));
        
        if (oldToken is not null)
        {
            oldToken.RevocationDate = DateTime.UtcNow;
            
            _refreshTokenRepository.UpdateExistingEntity(oldToken);
        }

        var invalidUserTokens = user.RefreshTokens.Where(t => !t.IsValid);
        
        if (!invalidUserTokens.IsNullOrEmpty())
            _refreshTokenRepository.RemoveRangeOfExistingEntities(invalidUserTokens);
        
        RemoveTokenCookies();
        
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register([FromBody]RegistrationDto registrationData)
    {
        if (IsInvalidModel(out var badRequest,
                "Error occured during registration of a new user!",
                ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList()))
            return badRequest;

        if (await IsRegisteredEmail(registrationData.Email))
            return BadRequest(new ApiValidationErrorResponse(
                "Error occured during registration of a new user!")
            {
                Errors = new [] { "User with entered email is already registered!" }
            });
        
        var createdUser = CreateFormattedUser(registrationData);

        createdUser.UserName += createdUser.Id.ToString()[..8]; 
        
        var userResult = await _userManager.CreateAsync(createdUser, registrationData.Password);

        if (!userResult.Succeeded) return BadRequest(new ApiResponse(
            400, "Error occured during registration of a new user!"));

        var user = await _userManager.FindByIdAsync(createdUser.Id.ToString());

        await _userManager.AddToRoleAsync(user!, (await _roleManager.FindByNameAsync("User"))!.Name!);

        var verificationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var verificationUrl = "https://localhost:7001" + Url.Action(
            nameof(VerifyEmail), "User",new { verificationToken, user.Email } );
        
        await SendNotificationLetterAsync(
            user, 
            "buyit.verify@gmail.com",
            "Registration at BuyIt!",
            EmailMessages.VerificationRequest,
            "Verify email address",
            verificationUrl);

        return Ok($"New user with email {user.Email} was registered successfully!" +
                  $" Waiting for email verification!");
    }

    [AllowAnonymous]
    [HttpPut("VerifyEmail")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> VerifyEmail(
        [FromQuery]string verificationToken, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user is null) return BadRequest(new ApiResponse(
            400, "User does not exist!"));

        var verificationResult = await _userManager.ConfirmEmailAsync(user, verificationToken);
        
        if (!verificationResult.Succeeded) return BadRequest(new ApiResponse(
            400, "Email verification was not performed!"));
        
        await SendNotificationLetterAsync(
            user, 
            "buyit.verify@gmail.com",
            "Email address confirmation at BuyIt!",
            EmailMessages.SuccessfulVerification,
            null,
            null);
        
        return Ok($"Email ({email}) was verified successfully!");
    }

    [AllowAnonymous]
    [HttpPut("RefreshAccessToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> RefreshAccessToken()
    {
        var refreshToken = Request.Cookies["UserRefreshToken"];
        
        var user = await _userManager.Users
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
            
            _refreshTokenRepository.UpdateExistingEntity(oldToken);
        }

        var invalidUserTokens = user.RefreshTokens.Where(t => !t.IsValid);
        
        if (!invalidUserTokens.IsNullOrEmpty())
            _refreshTokenRepository.RemoveRangeOfExistingEntities(invalidUserTokens);
        
        await GenerateAccessTokenAsync(user);
        await SetRefreshTokenAsync(user);
        
        return Ok();
    }

    private void RemoveTokenCookies()
    {
        Response.Cookies.Delete("UserAccessToken");
        Response.Cookies.Delete("UserRefreshToken");
    }

    [AllowAnonymous]
    [HttpGet("EmailStatus")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> GetEmailStatus(
        [FromQuery] string email) =>
        Ok(await IsRegisteredEmail(email));

    private async Task<bool> IsRegisteredEmail(string email) => 
        await _userManager.FindByEmailAsync(email) is not null;
    
    private bool IsInvalidModel(
        out ActionResult badRequest, string errorMessage, IEnumerable<string> errors)
    {
        if (!ModelState.IsValid)
        {
            badRequest = BadRequest(new ApiValidationErrorResponse(errorMessage)
            {
                Errors = errors
            });
            
            return true;
        }

        badRequest = null;
        return false;
    }

    private User CreateFormattedUser(RegistrationDto registrationData) =>
        new()
        {
            FirstName = new CultureInfo("en-US").TextInfo.ToTitleCase(registrationData.FirstName),
            MiddleName = new CultureInfo("en-US").TextInfo.ToTitleCase(registrationData.MiddleName),
            LastName = new CultureInfo("en-US").TextInfo.ToTitleCase(registrationData.LastName),
            Email = registrationData.Email,
            PhoneNumber = registrationData.PhoneNumber,
            UserName = $"@{registrationData.FirstName.ToLower()}{registrationData.LastName.ToLower()}-",
            WishListId = Guid.NewGuid()
        };
    
    private async Task<UserDto> GetUserDataResponse(User user) =>
        new()
        {
            DisplayedName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Roles = await _userManager.GetRolesAsync(user),
            BasketId = user.BasketId,
            WishListId = user.WishListId,
            ComparisonListId = user.ComparisonListId
        };
    
    private async Task SetRefreshTokenAsync(User user)
    {
        var refreshToken = _tokenService.CreateRefreshToken();
        refreshToken.UserId = user.Id;
        await _refreshTokenRepository.AddNewEntityAsync(refreshToken);
        
        user.RefreshTokens.Add(refreshToken);
        
        await _userManager.UpdateAsync(user);

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
    
    private async Task GenerateAccessTokenAsync(User user)
    {
        var accessToken = _tokenService.CreateAccessToken(user);

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
    
    private async Task SendNotificationLetterAsync(User user, string senderEmail,
        string subject, string message, string buttonName, string buttonUrl)
    {
        var mailSender = new MailSender(senderEmail);

        await mailSender.SendEmailAsync(new EmailOptions(
            user.Email!, subject, message, buttonName, buttonUrl));
    }
}