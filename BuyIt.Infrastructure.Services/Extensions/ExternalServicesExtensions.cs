using BuyIt.Infrastructure.Services.Mailing;
using BuyIt.Infrastructure.Services.TokenGeneration;
using Domain.Contracts.ServiceRelated;
using Domain.Contracts.TokenRelated;
using Microsoft.Extensions.DependencyInjection;

namespace BuyIt.Infrastructure.Services.Extensions;

public static class ExternalServicesExtensions
{
    public static IServiceCollection AddRequiredExternalServiceCollection
        (this IServiceCollection serviceCollection) 
        // Method that contains all services that will be used in application building process.
        // Additional services can be added in this method in the future.
        // Altering or removal of services can be performed at your own risk.
    {
        AddRequiredServices(serviceCollection);
        
        return serviceCollection;
    }
    
    private static void AddRequiredServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IConfirmationTokenService, ConfirmationTokenService>();
        serviceCollection.AddScoped<IAuthenticationTokenService, AuthenticationTokenService>();
        serviceCollection.AddScoped<IMailService, MailSender>();
    }
}