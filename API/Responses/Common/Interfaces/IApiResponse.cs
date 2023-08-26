namespace API.Responses.Common.Interfaces;

public interface IApiResponse
{
    int ResponseCode { get; set; }
    
    string ResponseMessage { get; set; }
}