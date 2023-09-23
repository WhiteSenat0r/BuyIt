using API.Extensions;
using AutoMapper;
using Core.Entities.Product;
using Core.Entities.Product.ProductSpecificationRelated;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Tests.Api.UnitTests.Extensions;

public class ApplicationServicesExtensionsTests
{
    [Fact]
    public void AddRequiredApplicationServiceCollection_AddsServicesToContainer()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        // Act
        serviceCollection.AddRequiredApplicationServiceCollection(configuration);

        // Assert
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var context = serviceProvider.GetRequiredService<StoreContext>();
        var productRepo = serviceProvider.GetRequiredService<IRepository<Product>>();
        var productTypeRepo = serviceProvider.GetRequiredService<IRepository<ProductType>>();
        var productManufacturerRepo = serviceProvider.GetRequiredService<IRepository<ProductManufacturer>>();
        var productRatingRepo = serviceProvider.GetRequiredService<IRepository<ProductRating>>();
        var productSpecificationRepo = serviceProvider.GetRequiredService<IRepository<ProductSpecification>>();
        var mapper = serviceProvider.GetRequiredService<IMapper>();
        
        Assert.NotNull(context);
        Assert.NotNull(productRepo);
        Assert.NotNull(productTypeRepo);
        Assert.NotNull(productManufacturerRepo);
        Assert.NotNull(productRatingRepo);
        Assert.NotNull(productSpecificationRepo);
        Assert.NotNull(mapper);
    }

    [Fact]
    public void AddRequiredApplicationServiceCollection_ConfiguresApiBehaviorOptions()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        // Act
        serviceCollection.AddRequiredApplicationServiceCollection(configuration);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var apiBehaviorOptions = serviceProvider.GetRequiredService<IOptions<ApiBehaviorOptions>>();

        // Assert
        Assert.NotNull(apiBehaviorOptions.Value.InvalidModelStateResponseFactory);
    }
}