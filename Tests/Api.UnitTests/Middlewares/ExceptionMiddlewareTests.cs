using System.Net;
using System.Text.Json;
using API.Middlewares.ExceptionHandlerMiddleware;
using API.Middlewares.ExceptionHandlerMiddleware.Common.Classes;
using API.Responses.Common.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Api.UnitTests.Middlewares;

public class ExceptionMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_ShouldHandleExceptionAndReturnApiResponse()
    {
        var loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
        var environmentMock = new Mock<IHostEnvironment>();
        var httpContext = new DefaultHttpContext
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };

        var exceptionMiddleware = new ExceptionMiddleware(
            (_) => throw new Exception("Unexpected Error"),
            loggerMock.Object,
            environmentMock.Object);

        await exceptionMiddleware.InvokeAsync(httpContext);

        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

        var response = JsonSerializer.Deserialize<ApiResponse>(responseText);

        Assert.NotNull(response);
        Assert.Equal((int)HttpStatusCode.InternalServerError, httpContext.Response.StatusCode);
        Assert.Equal("Unexpected Error", response.ResponseMessage);
    }

    [Fact]
    public async Task InvokeAsync_ShouldReturnApiExceptionInDevelopmentEnvironment()
    {
        var loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
        var environmentMock = new Mock<IHostEnvironment>();
        var httpContext = new DefaultHttpContext
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };

        environmentMock.Setup(env => env.EnvironmentName).Returns("Development");

        var exceptionMiddleware = new ExceptionMiddleware(
            (innerHttpContext) => throw new Exception("Unexpected Error"),
            loggerMock.Object,
            environmentMock.Object);

        await exceptionMiddleware.InvokeAsync(httpContext);

        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

        var response = JsonSerializer.Deserialize<ApiException>(responseText);

        Assert.NotNull(response);
        Assert.Equal((int)HttpStatusCode.InternalServerError, httpContext.Response.StatusCode);
        Assert.Equal("Unexpected Error", response.ResponseMessage);
    }
}