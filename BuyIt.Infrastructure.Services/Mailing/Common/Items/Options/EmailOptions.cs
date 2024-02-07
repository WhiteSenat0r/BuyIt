using Domain.Contracts.ServiceRelated;

namespace BuyIt.Infrastructure.Services.Mailing.Common.Items.Options;

public sealed class EmailOptions : IMailOptions
{
    public EmailOptions(string senderEmail, string receiverEmail, string subject,
        string messageBody, string? buttonName, string? buttonUrl)
    {
        SenderEmail = senderEmail;
        ReceiverEmail = receiverEmail;
        Subject = subject;
        MessageBody = messageBody;
        ButtonName = buttonName;
        ButtonUrl = buttonUrl;
    }
    
    public string SenderEmail { get; set; }
    public string ReceiverEmail { get; set; }
    public string Subject { get; set; }
    public string MessageBody { get; set; }
    public string? ButtonName { get; set; }
    public string? ButtonUrl { get; set; }
}