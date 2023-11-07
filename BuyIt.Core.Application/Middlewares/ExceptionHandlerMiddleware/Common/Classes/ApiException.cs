using Application.Responses.Common.Classes;

namespace Application.Middlewares.ExceptionHandlerMiddleware.Common.Classes;

public sealed class ApiException : ApiResponse
{
    public ApiException(int responseCode, string responseMessage, string responseDetails)
        : base(responseCode, responseMessage) => ResponseDetails = responseDetails;
    
    public string ResponseDetails { get; set; }
}