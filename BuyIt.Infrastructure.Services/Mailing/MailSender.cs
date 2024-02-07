using System.Net;
using System.Net.Mail;
using BuyIt.Infrastructure.Services.FileSystem;
using BuyIt.Infrastructure.Services.Mailing.Common.Items.Options;
using Domain.Contracts.ServiceRelated;
using Microsoft.IdentityModel.Tokens;

namespace BuyIt.Infrastructure.Services.Mailing;

public sealed class MailSender : IMailService
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;

    public MailSender()
    {
        _smtpHost = "smtp.gmail.com";
        _smtpPort = 587;
    }

    public async Task SendEmailAsync(IEnumerable<string> inputOptions)
    {
        var options = new EmailOptions(
            inputOptions.ElementAt(0),
            inputOptions.ElementAt(1),
            inputOptions.ElementAt(2),
            inputOptions.ElementAt(3),
            inputOptions.ElementAt(4),
            inputOptions.ElementAt(5));
        
        using var smtpClient = InstantiateSmtpClient(options);

        var relativePathConstructor = new RelativePathConstructor();
        
        var messageTemplate = await DeterminateMessageTemplateAsync(
            options, relativePathConstructor);

        AdjustMessageTemplate(options, ref messageTemplate);

        await smtpClient.SendMailAsync(new MailMessage(
            options.SenderEmail, options.ReceiverEmail,
            options.Subject, messageTemplate)
        {
            IsBodyHtml = true
        });
    }

    private void AdjustMessageTemplate(IMailOptions options, ref string messageTemplate)
    {
        if (!IsButtonlessMessage(options))
        {
            messageTemplate = messageTemplate.Replace("*buttonUrl*", options.ButtonUrl);
            messageTemplate = messageTemplate.Replace("*ButtonName*", options.ButtonName);
        }

        messageTemplate = messageTemplate.Replace("*ReplacedTextMessage*", options.MessageBody);
    }

    private async Task<string> DeterminateMessageTemplateAsync(
        IMailOptions options, RelativePathConstructor relativePathConstructor) =>
        IsButtonlessMessage(options) 
            ? await File.ReadAllTextAsync(relativePathConstructor.CreateFilePath(
                "BuyIt.Infrastructure.Services/Mailing/Common/Items/MessageTemplates" +
                "/HTML-Templates/ButtonlessMessageTemplate.html"))
            : await File.ReadAllTextAsync(relativePathConstructor.CreateFilePath(
                "BuyIt.Infrastructure.Services/Mailing/Common/Items/MessageTemplates/" +
                "HTML-Templates/ButtonMessageTemplate.html"));

    private bool IsButtonlessMessage(IMailOptions options) =>
        options.ButtonName.IsNullOrEmpty() 
        && options.ButtonUrl.IsNullOrEmpty();

    private SmtpClient InstantiateSmtpClient(IMailOptions options) =>
        new()
        {
            Host = _smtpHost,
            Port = _smtpPort,
            Credentials = new NetworkCredential(
                options.SenderEmail, EmailCredentials()[options.SenderEmail]),
            EnableSsl = true
        };

    private IDictionary<string, string> EmailCredentials() =>
        new Dictionary<string, string>
        {
            {
                "buyit.verify@gmail.com", "rlkh wpzy xifj ozri"
            }
        };
}