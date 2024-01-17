using Application.DataTransferObjects.IdentityRelated;
using Application.Responses;
using Application.Responses.Common.Classes;
using BuyIt.Infrastructure.Services.Mailing;
using BuyIt.Infrastructure.Services.Mailing.Common.Classes.Options;
using BuyIt.Presentation.WebAPI.Controllers.Common.Classes;
using Domain.Contracts.TokenRelated;
using Domain.Entities.IdentityRelated;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.IdentityRelated;

public class UserController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<UserRole> _roleManager;
    private readonly ITokenService _tokenService;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager,
        RoleManager<UserRole> roleManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }
    
    [HttpPost("login")]
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
        
        var passwordIsValid = await _signInManager.CheckPasswordSignInAsync(
            user, loginData.Password, false);
        
        if (!passwordIsValid.Succeeded) return Unauthorized(
            new ApiResponse(401, "Incorrect password!"));
        
        return Ok(new UserDto
        {
            DisplayedName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            Roles = await _userManager.GetRolesAsync(user)
        });
    }
    
    [HttpPost("register")]
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
        
        var createdUser = new User
        {
            FirstName = registrationData.FirstName,
            MiddleName = registrationData.MiddleName,
            LastName = registrationData.LastName,
            Email = registrationData.Email,
            PhoneNumber = registrationData.PhoneNumber,
            UserName = $"@{registrationData.FirstName.ToLower()}{registrationData.LastName.ToLower()}"
        };
        
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
            "Congratulations and welcome to BuyIt! We're excited to have you on board and thank you" +
            " for choosing us.<br><br>Your registration process is almost complete, but before we get started," +
            " we need to confirm your email address. Please click the button below to verify your account." +
            "<br><br>If you are unable to click the link, you can copy and paste it into your browser's address" +
            " bar.<br><br>Once your email address is verified, you'll gain full access to your account.<br><br>Thank" +
            " you for joining BuyIt! We look forward to serving you.",
            "Verify email address",
            verificationUrl);

        return Ok(new UserDto
        {
            DisplayedName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            Roles = await _userManager.GetRolesAsync(user)
        });
    }

    [HttpPut("verifyemail")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> VerifyEmail(
        [FromQuery]string verificationToken, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user is null) return BadRequest(new ApiResponse(400));

        var verificationResult = await _userManager.ConfirmEmailAsync(user, verificationToken);
        
        if (!verificationResult.Succeeded) return BadRequest(new ApiResponse(400));
        
        await SendNotificationLetterAsync(
            user, 
            "buyit.verify@gmail.com",
            "Email address confirmation at BuyIt!",
            "Congratulations! We're excited to inform you that you have successfully verified your email " +
            "address.<br><br>Your registration process is complete and you are ready to go shopping at BuyIt!" +
            "<br><br>We hope that our marketplace will be able to offer you everything what you need!<br><br>" +
            "Happy purchases and enjoy your time at BuyIt!",
            null,
            null);
        
        return Ok(new UserDto
        {
            DisplayedName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            Roles = await _userManager.GetRolesAsync(user)
        });
    }

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

    private async Task SendNotificationLetterAsync(User user, string senderEmail,
        string subject, string message, string buttonName, string buttonUrl)
    {
        var mailSender = new MailSender(senderEmail);

        await mailSender.SendEmailAsync(new EmailOptions(
            user.Email!, subject, message, buttonName, buttonUrl));
    }
}