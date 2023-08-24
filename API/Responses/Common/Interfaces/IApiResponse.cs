namespace API.Responses.Common.Interfaces;

public interface IApiResponse
{
    int ResponseCode { get; }
    
    string ResponseMessage { set; }
}