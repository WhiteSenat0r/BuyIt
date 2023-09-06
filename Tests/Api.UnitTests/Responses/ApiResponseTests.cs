using API.Responses.Common.Classes;

namespace Tests.Api.UnitTests.Responses;

public class ApiResponseTests
{
    [Theory]
    [InlineData(400, "Bad Request")]
    [InlineData(401, "Unauthorized")]
    [InlineData(402, "Payment Required")]
    [InlineData(403, "Forbidden")]
    [InlineData(404, "Not found")]
    [InlineData(408, "Request Timeout")]
    [InlineData(429, "Too Many Requests")]
    [InlineData(500, "Internal Server Error")]
    [InlineData(502, "Bad Gateway")]
    [InlineData(504, "Gateway Timeout")]
    [InlineData(520, "Unknown Error")]
    [InlineData(521, "Web Server Is Down")]
    [InlineData(524, "A Timeout Occurred")]
    [InlineData(999, "Unexpected Error")]
    public void Constructor_ShouldSetResponseCodeAndMessage(int responseCode, string expectedResponseMessage)
    {
        var apiResponse = new ApiResponse(responseCode, null);

        Assert.Equal(responseCode, apiResponse.ResponseCode);
        Assert.Equal(expectedResponseMessage, apiResponse.ResponseMessage);
    }

    [Fact]
    public void Constructor_ShouldSetDefaultMessageForNullResponseMessage()
    {
        var apiResponse = new ApiResponse(400, null);

        Assert.Equal("Bad Request", apiResponse.ResponseMessage);
    }

    [Fact]
    public void GetDefaultResponseMessage_ShouldReturnUnexpectedErrorForUnknownCode()
    {
        var apiResponse = new ApiResponse(999, null);

        var defaultResponseMessage = apiResponse.ResponseMessage;

        Assert.Equal("Unexpected Error", defaultResponseMessage);
    }
}