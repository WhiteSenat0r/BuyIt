using Application.Middlewares.ExceptionHandlerMiddleware.Common.Classes;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.Responses;

public class ApiExceptionTests
{
    [Fact]
    public void Constructor_ShouldSetResponseCodeAndResponseMessageAndResponseDetails()
    {
        var responseCode = 500;
        var responseMessage = "Internal Server Error";
        var responseDetails = "An internal error occurred.";

        var apiException = new ApiException(responseCode, responseMessage, responseDetails);

        Assert.Equal(responseCode, apiException.ResponseCode);
        Assert.Equal(responseMessage, apiException.ResponseMessage);
        Assert.Equal(responseDetails, apiException.ResponseDetails);
    }

    [Fact]
    public void ResponseDetailsProperty_ShouldSetAndGetResponseDetails()
    {
        var apiException = new ApiException
            (500, "Internal Server Error", "An internal error occurred.");
        var responseDetails = "Updated response details.";

        apiException.ResponseDetails = responseDetails;
        var retrievedResponseDetails = apiException.ResponseDetails;

        Assert.Equal(responseDetails, retrievedResponseDetails);
    }
}