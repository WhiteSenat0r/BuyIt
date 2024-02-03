using System.Text.Json.Serialization;
using Application.Helpers.SpecificationResolver;
using Application.Responses;
using BuyIt.Infrastructure.Services.TokenGeneration;
using Domain.Contracts.Common;
using Domain.Contracts.ProductListRelated;
using Domain.Contracts.RepositoryRelated.NonRelational;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Contracts.TokenRelated;
using Domain.Entities.IdentityRelated;
using Domain.Entities.ProductListRelated;
using Domain.Entities.ProductRelated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.RepositoryFactories;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.IdentityRelated;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;
using StackExchange.Redis;

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

        AddRequiredServices(serviceCollection);

        AddApiBehaviourConfiguration(serviceCollection);

        return serviceCollection;
    }

    private static void AddRequiredServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ProductSpecificationFilterResolver>();
        serviceCollection.AddScoped<IConfirmationTokenService, ConfirmationTokenService>();
        serviceCollection.AddScoped<IAuthenticationTokenService, AuthenticationTokenService>();
    }

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
        AddRepository<RefreshToken, RefreshTokenRepositoryFactory>(serviceCollection);
        
        AddNonRelationalRepository
            <BasketItem, ProductList<BasketItem>, BasketRepositoryFactory>(serviceCollection);
        AddNonRelationalRepository
            <WishedItem, ProductList<WishedItem>, WishlistRepositoryFactory>(serviceCollection);
        AddNonRelationalRepository
            <ComparedItem, ProductList<ComparedItem>, ComparisonRepositoryFactory>(serviceCollection);
    }

    private static void AddRepository<TEntity, TRepository>(IServiceCollection serviceCollection) 
        where TEntity : class, IEntity<Guid> where TRepository : RepositoryFactory<TEntity>, new() =>
        serviceCollection.AddScoped<IRepository<TEntity>>(provider =>
            new TRepository().Create(
                provider.GetService<StoreContext>()!));

    private static void AddNonRelationalRepository<TItem, TList, TRepositoryFactory>(
        IServiceCollection serviceCollection)
        where TItem : class, IProductListItem, new()
        where TList : class, IProductList<TItem>, new()
        where TRepositoryFactory : NonRelationalRepositoryFactory<TList>, new()
    {
        serviceCollection.AddScoped<INonRelationalRepository<TList>>(provider =>
            new TRepositoryFactory().Create(
                provider.GetService<IConnectionMultiplexer>()!));
    }

    private static void AddRequiredDbContexts(
        IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<StoreContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });

        serviceCollection.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(
            ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection")!)));
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