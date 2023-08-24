using API.Responses.Common.Classes;

namespace API.Middlewares.ExceptionHandlerMiddleware.Common.Classes;

public class ApiException : ApiResponse
{
    public ApiException(int responseCode, string responseMessage, string responseDetails)
        : base(responseCode, responseMessage) => ResponseDetails = responseDetails;
    
    public string ResponseDetails { get; set; }
}