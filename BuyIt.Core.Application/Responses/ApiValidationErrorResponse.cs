using Application.Responses.Common.Classes;

namespace Application.Responses;

public class ApiValidationErrorResponse : ApiResponse
{
    public ApiValidationErrorResponse(string responseMessage)
        : base(400, responseMessage) { }

    public IEnumerable<string> Errors { get; set; }
}