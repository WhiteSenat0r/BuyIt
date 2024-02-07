using Domain.Contracts.Common;
using Domain.Contracts.ProductListRelated;
using Domain.Contracts.RepositoryRelated.NonRelational;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities.IdentityRelated;
using Domain.Entities.ProductListRelated;
using Domain.Entities.ProductRelated;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.Factories.NonRelationalRepositoryFactories.RepositoryFactories;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.IdentityRelated;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;
using StackExchange.Redis;

namespace Persistence.Extensions;

public static class PersistenceServicesExtensions
{
    public static IServiceCollection AddRequiredPersistenceServiceCollection
        (this IServiceCollection serviceCollection, IConfiguration configuration) 
        // Method that contains all services that will be used in application building process.
        // Additional services can be added in this method in the future.
        // Altering or removal of services can be performed at your own risk.
    {
        AddRequiredDbContexts(serviceCollection, configuration);
        AddIdentityPersistenceServices(serviceCollection);
        AddRequiredRepositories(serviceCollection);

        return serviceCollection;
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
    
    private static void AddIdentityPersistenceServices(IServiceCollection serviceCollection)
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