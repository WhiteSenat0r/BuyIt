using API.Responses.Common.Interfaces;

namespace API.Responses.Common.Classes;

public class ApiResponse : IApiResponse
{
    public ApiResponse(int responseCode, string responseMessage)
    {
        ResponseCode = responseCode;
        ResponseMessage = responseMessage ?? GetDefaultResponseMessage();
    }
    
    public int ResponseCode { get; }
    
    public string ResponseMessage { get; set; }

    private string GetDefaultResponseMessage() => 
        ResponseCode switch
    {
        400 => "Bad Request", 401 => "Unauthorized",
        402 => "Payment Required", 403 => "Forbidden",
        404 => "Not found", 408 => "Request Timeout",
        429 => "Too Many Requests", 500 => "Internal Server Error",
        502 => "Bad Gateway", 504 => "Gateway Timeout",
        520 => "Unknown Error", 521 => "Web Server Is Down",
        524 => "A Timeout Occurred", _ => "Unexpected Error"
    };
}