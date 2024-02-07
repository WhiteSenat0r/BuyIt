using System.Text.Json.Serialization;
using Application.Helpers.SpecificationResolver;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddRequiredApplicationServiceCollection
        (this IServiceCollection serviceCollection) 
        // Method that contains all services that will be used in application building process.
        // Additional services can be added in this method in the future.
        // Altering or removal of services can be performed at your own risk.
    {
        AddRequiredControllers(serviceCollection);

        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        AddRequiredServices(serviceCollection);

        AddApiBehaviourConfiguration(serviceCollection);

        return serviceCollection;
    }

    private static void AddRequiredServices(IServiceCollection serviceCollection) 
        => serviceCollection.AddScoped<ProductSpecificationFilterResolver>();

    private static void AddApiBehaviourConfiguration(IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(option =>
        {
            option.AddPolicy("ApplicationCorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200");
            });
        });
        
        serviceCollection.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors)
                    .Select(e => e.ErrorMessage).ToList();

                var errorResponse = new ApiValidationErrorResponse(null)
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });
    }

    private static void AddRequiredControllers(IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers();

        serviceCollection.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    }
}