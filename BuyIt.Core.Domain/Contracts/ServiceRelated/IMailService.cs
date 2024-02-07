namespace Domain.Contracts.ServiceRelated;

public interface IMailService
{ 
    Task SendEmailAsync(IEnumerable<string> inputOptions);
}