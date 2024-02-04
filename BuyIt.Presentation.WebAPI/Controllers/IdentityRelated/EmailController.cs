using Application.Responses.Common.Classes;
using BuyIt.Infrastructure.Services.Mailing;
using BuyIt.Infrastructure.Services.Mailing.Common.Items.MessageTemplates.MessageTemplates;
using BuyIt.Infrastructure.Services.Mailing.Common.Items.Options;
using BuyIt.Presentation.WebAPI.Controllers.IdentityRelated.Common.Classes;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Contracts.TokenRelated;
using Domain.Entities.IdentityRelated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.IdentityRelated;

public class EmailController : BaseIdentityRelatedController
{
    private readonly IConfirmationTokenService _confirmationTokenService;

    public EmailController(UserManager<User> userManager,
        IAuthenticationTokenService authenticationTokenService,
        IRepository<RefreshToken> refreshTokenRepository,
        IConfirmationTokenService confirmationTokenService) 
        : base(userManager, authenticationTokenService, refreshTokenRepository) =>
        _confirmationTokenService = confirmationTokenService;

    [AllowAnonymous]
    [HttpPost("SendEmailConfirmationLetter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> SendEmailConfirmationLetter([FromQuery] string email)
    {
        var user = await UserManager.FindByEmailAsync(email);
        
        await SendEmailConfirmationLetterAsync(user);
        
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("SendEmailSuccessfulVerificationLetter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> SendEmailSuccessfulVerificationLetter([FromQuery] string email)
    {
        var user = await UserManager.FindByEmailAsync(email);
        
        if (user is null) return BadRequest(new ApiResponse(
            400, "User does not exist!"));
        
        await SendSuccessfulEmailConfirmationLetterAsync(user);
        
        return Ok();
    }

    private async Task SendSuccessfulEmailConfirmationLetterAsync(User user) =>
        await SendNotificationLetterAsync(
            user, 
            "buyit.verify@gmail.com",
            "Email address confirmation at BuyIt!",
            EmailMessages.SuccessfulVerification,
            null,
            null);

    private async Task SendEmailConfirmationLetterAsync(User user)
    {
        var verificationToken = _confirmationTokenService.CreateToken(user);
        
        var verificationUrl =  $"https://localhost:4200/account/" +
                              $"verify-email?address={user.Email}&token={verificationToken}";
        
        await SendNotificationLetterAsync(
            user, 
            "buyit.verify@gmail.com",
            "Registration at BuyIt!",
            EmailMessages.VerificationRequest,
            "Verify email address",
            verificationUrl);
    }
    
    private async Task SendNotificationLetterAsync(User user, string senderEmail,
        string subject, string message, string buttonName, string buttonUrl)
    {
        var mailSender = new MailSender(senderEmail);

        await mailSender.SendEmailAsync(new EmailOptions(
            user.Email!, subject, message, buttonName, buttonUrl));
    }
}