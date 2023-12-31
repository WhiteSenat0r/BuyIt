﻿using System.Net;
using System.Text.Json;
using Application.Middlewares.ExceptionHandlerMiddleware.Common.Classes;
using Application.Responses.Common.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Middlewares.ExceptionHandlerMiddleware;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionMiddleware
        (RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = GetSuitableResponse(e);

            var serializedLog = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await httpContext.Response.WriteAsync(serializedLog);
        }
    }

    private object GetSuitableResponse(Exception e) =>
        _environment.IsDevelopment()
            ? new ApiException
                ((int)HttpStatusCode.InternalServerError, e.Message, e.StackTrace!)
            : new ApiResponse
                ((int)HttpStatusCode.InternalServerError, null);
}