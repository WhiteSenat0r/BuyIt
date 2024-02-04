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
using Microsoft.IdentityModel.Tokens;

namespace BuyIt.Presentation.WebAPI.Controllers.IdentityRelated;

public class CredentialsController : BaseIdentityRelatedController
{
    private readonly IConfirmationTokenService _confirmationTokenService;

    public CredentialsController(UserManager<User> userManager,
        IAuthenticationTokenService authenticationTokenService,
        IRepository<RefreshToken> refreshTokenRepository,
        IConfirmationTokenService confirmationTokenService) 
        : base(userManager, authenticationTokenService, refreshTokenRepository) =>
        _confirmationTokenService = confirmationTokenService;

    [AllowAnonymous]
    [HttpPut("ChangePasswordByConfirmationToken")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> ChangePasswordByConfirmationToken(
        [FromQuery] PasswordAlterationDto passwordAlterationDto)
    {
        if (passwordAlterationDto.Email.IsNullOrEmpty())
            return BadRequest(new ApiResponse(400, "Email is invalid!"));
        
        if (passwordAlterationDto.Token.IsNullOrEmpty())
            return BadRequest(new ApiResponse(400, "Token is invalid!"));
        
        if (passwordAlterationDto.NewPassword.IsNullOrEmpty())
            return BadRequest(new ApiResponse(400, "New password is invalid!"));
        
        if (!await IsRegisteredEmail(passwordAlterationDto.Email))
            return BadRequest(new ApiValidationErrorResponse(
                "Error occured during password altering!")
            {
                Errors = new [] { "User with entered email is not registered!" }
            });

        var user = await UserManager.FindByEmailAsync(passwordAlterationDto.Email);
        
        if (!user.EmailConfirmed) 
            return BadRequest(new ApiResponse(
                400, "User's email is not verified!"));

        var passwordRemovalResult = await UserManager.RemovePasswordAsync(user);
        
        if (!passwordRemovalResult.Succeeded)
            return BadRequest(new ApiResponse(
                400, "Password was not removed!"));
        
        var passwordChangeResult = await UserManager.AddPasswordAsync(user, passwordAlterationDto.NewPassword);
        
        if (!passwordChangeResult.Succeeded)
            return BadRequest(new ApiResponse(
                400, "Password was not set!"));
        
        return Ok(await GetUserDataResponse(user));
    }
    
    [AllowAnonymous]
    [HttpPut("VerifyEmail")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> VerifyEmail(
        [FromQuery]string verificationToken, [FromQuery] string email)
    {
        var user = await UserManager.FindByEmailAsync(email);
        
        if (user is null) return BadRequest(new ApiResponse(
            400, "User does not exist!"));

        if (_confirmationTokenService.IsValidConfirmationToken(user, verificationToken))
            user.EmailConfirmed = true;
        else
            return BadRequest(new ApiResponse(
                400, "Invalid verification token!"));
        
        var verificationResult = await UserManager.UpdateAsync(user);
        
        if (!verificationResult.Succeeded) return BadRequest(new ApiResponse(
            400, "Email verification was not performed!"));
        
        return Ok(await GetUserDataResponse(user));
    }

    private async Task<bool> IsRegisteredEmail(string email) => 
        await UserManager.FindByEmailAsync(email) is not null;
}