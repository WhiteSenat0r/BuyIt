using Application.Responses;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.Responses;

public class ApiValidationErrorResponseTests
{
    [Fact]
    public void Constructor_ShouldSetResponseCodeAndResponseMessage()
    {
        var responseMessage = "Validation Failed";

        var validationErrorResponse = new ApiValidationErrorResponse(responseMessage);

        Assert.Equal(400, validationErrorResponse.ResponseCode);
        Assert.Equal(responseMessage, validationErrorResponse.ResponseMessage);
    }

    [Fact]
    public void ErrorsProperty_ShouldSetAndGetErrors()
    {
        var validationErrorResponse = new ApiValidationErrorResponse("Validation Failed");
        var errors = new List<string> { "Error 1", "Error 2" };

        validationErrorResponse.Errors = errors;
        var retrievedErrors = validationErrorResponse.Errors;

        Assert.Equal(errors, retrievedErrors);
    }
}