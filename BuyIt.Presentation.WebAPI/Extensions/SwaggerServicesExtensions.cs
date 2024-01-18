using Microsoft.OpenApi.Models;

namespace BuyIt.Presentation.WebAPI.Extensions;

public static class SwaggerServicesExtensions
{
    public static IServiceCollection AddRequiredSwaggerServiceCollection
        (this IServiceCollection serviceCollection, IConfiguration configuration) 
        // Method that contains all services that will be used in application building process.
        // Additional services can be added in this method in the future.
        // Altering or removal of services can be performed at your own risk.
    {
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(configuration =>
        {
            var securitySchema = GetApiSecuritySchema();
            
            configuration.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = GetApiSecurityRequirement(securitySchema);
            
            configuration.AddSecurityRequirement(securityRequirement);
        });

        return serviceCollection;
    }

    public static IApplicationBuilder UseRequiredSwaggerServiceCollection
        (this IApplicationBuilder application) 
        // Method that initializes all services that will be used in application building process.
        // Additional services can be added in this method in the future.
        // Altering or removal of services can be performed at your own risk.
    {
        application.UseSwagger();
        application.UseSwaggerUI();
        
        return application;
    }
    
    private static OpenApiSecurityScheme GetApiSecuritySchema() =>
        new()
        {
            Description = "JWT Auth Bearer Scheme",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        };
    
    private static OpenApiSecurityRequirement GetApiSecurityRequirement(
        OpenApiSecurityScheme securitySchema) =>
        new()
        {
            { securitySchema, new [] { "Bearer" } }
        };
}