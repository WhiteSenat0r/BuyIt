namespace BuyIt.Infrastructure.Services.Mailing.Common.Classes.Options;

public sealed class EmailOptions
{
    public EmailOptions(string receiverEmail, string subject,
        string messageBody, string? buttonName, string? buttonUrl)
    {
        ReceiverEmail = receiverEmail;
        Subject = subject;
        MessageBody = messageBody;
        ButtonName = buttonName;
        ButtonUrl = buttonUrl;
    }
    
    public string ReceiverEmail { get; set; }
    public string Subject { get; set; }
    public string MessageBody { get; set; }
    public string? ButtonName { get; set; }
    public string? ButtonUrl { get; set; }
}