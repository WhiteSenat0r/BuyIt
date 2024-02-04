using System.Globalization;
using System.Security.Claims;
using Application.DataTransferObjects.IdentityRelated;
using Application.Responses;
using Application.Responses.Common.Classes;
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

public class UserController : BaseIdentityRelatedController
{
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<UserRole> _roleManager;

    public UserController(UserManager<User> userManager, 
        IAuthenticationTokenService authenticationTokenService,
        IRepository<RefreshToken> refreshTokenRepository,
        SignInManager<User> signInManager,
        RoleManager<UserRole> roleManager) 
        : base(userManager, authenticationTokenService, refreshTokenRepository)
    {
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [Authorize]
    [HttpGet("CurrentUser")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        var user = await UserManager.FindByEmailAsync(userEmail);
        
        return Ok(await GetUserDataResponse(user));
    }
   
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

        var user = await UserManager.FindByEmailAsync(loginData.Email);
        
        if (!user.EmailConfirmed) 
            return Unauthorized(new ApiResponse(
                401, "The email is not verified!"));
        
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
        var user = await UserManager.Users.Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.UserName.Equals(
                User.FindFirstValue(ClaimTypes.Name)));

        if (user is null) 
            return Unauthorized(new ApiResponse(
                401, "User is not authorized!"));

        var refreshToken = Request.Cookies["UserRefreshToken"];

        var oldToken = user.RefreshTokens.SingleOrDefault(t => t.Value.Equals(refreshToken));
        
        if (oldToken is not null)
        {
            oldToken.RevocationDate = DateTime.UtcNow;
            
            RefreshTokenRepository.UpdateExistingEntity(oldToken);
        }

        var invalidUserTokens = user.RefreshTokens.Where(t => !t.IsValid);
        
        if (!invalidUserTokens.IsNullOrEmpty())
            RefreshTokenRepository.RemoveRangeOfExistingEntities(invalidUserTokens);
        
        RemoveTokenCookies();
        
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> Register([FromBody]RegistrationDto registrationData)
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
        
        if (await IsRegisteredPhoneNumber(registrationData.PhoneNumber))
            return BadRequest(new ApiValidationErrorResponse(
                "Error occured during registration of a new user!")
            {
                Errors = new [] { "User with entered phone number is already registered!" }
            });
        
        var createdUser = CreateFormattedUser(registrationData);

        createdUser.UserName += createdUser.Id.ToString()[..8]; 
        
        var userResult = await UserManager.CreateAsync(createdUser, registrationData.Password);

        if (!userResult.Succeeded) 
            return BadRequest(new ApiResponse(
                400, "Error occured during registration of a new user!"));

        var user = await UserManager.FindByIdAsync(createdUser.Id.ToString());

        await UserManager.AddToRoleAsync(user!, (await _roleManager.FindByNameAsync("User"))!.Name!);

        return Ok(await GetUserDataResponse(user));
    }

    private async Task<bool> IsRegisteredEmail(string email) => 
        await UserManager.FindByEmailAsync(email) is not null;
    
    private async Task<bool> IsRegisteredPhoneNumber(string phoneNumber) => 
        await UserManager.Users.SingleOrDefaultAsync(
            u => u.PhoneNumber.Equals(phoneNumber)) is not null;
    
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
}