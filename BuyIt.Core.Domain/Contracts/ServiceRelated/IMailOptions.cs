namespace Domain.Contracts.ServiceRelated;

public interface IMailOptions
{ 
    string SenderEmail { get; set; }
    string ReceiverEmail { get; set; }
    string Subject { get; set; }
    string MessageBody { get; set; }
    string ButtonName { get; set; }
    string ButtonUrl { get; set; }
}