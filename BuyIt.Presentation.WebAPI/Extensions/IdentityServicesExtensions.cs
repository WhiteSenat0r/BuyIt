using System.Text;
using Domain.Entities.IdentityRelated;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;

namespace BuyIt.Presentation.WebAPI.Extensions;

public static class IdentityServicesExtensions
{
    public static IServiceCollection AddRequiredIdentityServiceCollection
        (this IServiceCollection serviceCollection, IConfiguration configuration) 
        // Method that contains all services that will be used in application building process.
        // Additional services can be added in this method in the future.
        // Altering or removal of services can be performed at your own risk.
    {
        AddPrimaryIdentityServices(serviceCollection);
        OverrideAuthenticationScheme(serviceCollection);
        AddJwtAuthenticationOptions(serviceCollection, configuration);
        serviceCollection.AddAuthorization();
        
        return serviceCollection;
    }

    private static void AddJwtAuthenticationOptions(
        IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "UserAccessToken";
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Token:Key"]!)),
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["UserAccessToken"];
                        return Task.CompletedTask;
                    }
                };
            });
    }

    private static void OverrideAuthenticationScheme(IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        });
    }

    private static void AddPrimaryIdentityServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<User, UserRole>()
            .AddEntityFrameworkStores<StoreContext>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider)
            .AddSignInManager();

        serviceCollection.Configure<SignInOptions>(options =>
        {
            options.RequireConfirmedEmail = true;
        });
    }
}