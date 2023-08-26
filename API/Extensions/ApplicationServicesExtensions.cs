using System.Text.Json.Serialization;
using API.Responses;
using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.Factories.ProductRelated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddRequiredApplicationServiceCollection
        (this IServiceCollection serviceCollection, IConfiguration configuration) 
        // Method that contains all services that will be used in application building process.
        // Additional services can be added in this method in the future.
        // Altering or removal of services can be performed at your own risk.
    {
        AddRequiredControllers(serviceCollection);

        AddSwaggerItems(serviceCollection);

        AddRequiredDbContexts(serviceCollection, configuration);

        AddRequiredRepositories(serviceCollection);

        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        AddApiBehaviourConfiguration(serviceCollection);

        return serviceCollection;
    }

    private static void AddApiBehaviourConfiguration(IServiceCollection serviceCollection)
    {
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

    private static void AddRequiredRepositories(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRepository<Product>>(provider =>
            new ProductRepositoryFactory().Create(
                provider.GetService<StoreContext>()!));

        serviceCollection.AddScoped<IRepository<ProductType>>(provider =>
            new ProductTypeRepositoryFactory().Create(
                provider.GetService<StoreContext>()!));

        serviceCollection.AddScoped<IRepository<ProductManufacturer>>(provider =>
            new ProductManufacturerRepositoryFactory().Create(
                provider.GetService<StoreContext>()!));

        serviceCollection.AddScoped<IRepository<ProductRating>>(provider =>
            new ProductRatingRepositoryFactory().Create(
                provider.GetService<StoreContext>()!));
    }

    private static void AddRequiredDbContexts(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<StoreContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }

    private static void AddSwaggerItems(IServiceCollection serviceCollection)
    {
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
    }

    private static void AddRequiredControllers(IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers();

        serviceCollection.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    }
}