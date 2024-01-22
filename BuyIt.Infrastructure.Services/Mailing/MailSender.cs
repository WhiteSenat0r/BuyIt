using System.Net;
using System.Net.Mail;
using BuyIt.Infrastructure.Services.FileSystem;
using BuyIt.Infrastructure.Services.Mailing.Common.Items.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuyIt.Infrastructure.Services.Mailing;

public sealed class MailSender
{
    private readonly string _senderEmail;
    private readonly string _senderAuthCode;
    private readonly string _smtpHost;
    private readonly int _smtpPort;

    public MailSender(string senderEmail)
    {
        _senderEmail = senderEmail;
        _senderAuthCode = EmailCredentials()[senderEmail];
        _smtpHost = "smtp.gmail.com";
        _smtpPort = 587;
    }

    public async Task SendEmailAsync(EmailOptions options)
    {
        using var smtpClient = InstantiateSmtpClient();

        var relativePathConstructor = new RelativePathConstructor();
        
        var messageTemplate = await DeterminateMessageTemplateAsync(
            options, relativePathConstructor);

        AdjustMessageTemplate(options, ref messageTemplate);

        await smtpClient.SendMailAsync(new MailMessage(
            _senderEmail, options.ReceiverEmail,
            options.Subject, messageTemplate)
        {
            IsBodyHtml = true
        });
    }

    private void AdjustMessageTemplate(EmailOptions options, ref string messageTemplate)
    {
        if (!IsButtonlessMessage(options))
        {
            messageTemplate = messageTemplate.Replace("*buttonUrl*", options.ButtonUrl);
            messageTemplate = messageTemplate.Replace("*ButtonName*", options.ButtonName);
        }

        messageTemplate = messageTemplate.Replace("*ReplacedTextMessage*", options.MessageBody);
    }

    private async Task<string> DeterminateMessageTemplateAsync(
        EmailOptions options, RelativePathConstructor relativePathConstructor) =>
        IsButtonlessMessage(options) 
            ? await File.ReadAllTextAsync(relativePathConstructor.CreateFilePath(
                "BuyIt.Infrastructure.Services/Mailing/Common/Items/MessageTemplates" +
                "/HTML-Templates/ButtonlessMessageTemplate.html"))
            : await File.ReadAllTextAsync(relativePathConstructor.CreateFilePath(
                "BuyIt.Infrastructure.Services/Mailing/Common/Items/MessageTemplates/" +
                "HTML-Templates/ButtonMessageTemplate.html"));

    private bool IsButtonlessMessage(EmailOptions options) =>
        options.ButtonName.IsNullOrEmpty() 
        && options.ButtonUrl.IsNullOrEmpty();

    private SmtpClient InstantiateSmtpClient() =>
        new()
        {
            Host = _smtpHost,
            Port = _smtpPort,
            Credentials = new NetworkCredential(
                _senderEmail, _senderAuthCode),
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