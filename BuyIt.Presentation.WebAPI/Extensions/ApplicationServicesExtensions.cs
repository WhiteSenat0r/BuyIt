using System.Text.Json.Serialization;
using Application.Helpers;
using Application.Responses;
using Domain.Contracts.Common;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Factories.Common.Classes;
using Persistence.Repositories.Factories.ProductRelated;

namespace BuyIt.Presentation.WebAPI.Extensions;

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

        serviceCollection.AddSingleton<ProductSpecificationFilterResolver>();

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

        serviceCollection.AddCors(option =>
        {
            option.AddPolicy("ApplicationCorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
            });
        });
    }

    private static void AddRequiredRepositories(IServiceCollection serviceCollection)
    {
        AddRepository<Product, ProductRepositoryFactory>(serviceCollection);
        AddRepository<ProductType, ProductTypeRepositoryFactory>(serviceCollection);
        AddRepository<ProductManufacturer, ProductManufacturerRepositoryFactory>(serviceCollection);
        AddRepository<ProductRating, ProductRatingRepositoryFactory>(serviceCollection);
        AddRepository<ProductSpecification, ProductSpecificationRepositoryFactory>(serviceCollection);
        AddRepository<ProductSpecificationCategory, 
            ProductSpecificationCategoryRepositoryFactory>(serviceCollection);
        AddRepository<ProductSpecificationAttribute, 
            ProductSpecificationAttributeRepositoryFactory>(serviceCollection);
        AddRepository<ProductSpecificationValue, 
            ProductSpecificationValueRepositoryFactory>(serviceCollection);
    }

    private static void AddRepository<TEntity, TRepository>(IServiceCollection serviceCollection) 
        where TEntity : class, IEntity<Guid> where TRepository : RepositoryFactory<TEntity>, new()
    {
        serviceCollection.AddScoped<IRepository<TEntity>>(provider =>
            new TRepository().Create(
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