using Application.DataTransferObjects.ProductRelated;
using Application.Helpers;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities.ProductRelated;
using Microsoft.Extensions.Configuration;
using Persistence.Contexts;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.Helpers.Resolvers;

public class ProductUrlResolverTests
{
    private StoreContext _context = null!;
    private IRepository<Product> _repository = null!;
    
    [Fact]
    public void Resolve_ReturnsMainImagesUrls_WhenDestinationIsNotGeneralizedProductDto()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ApiImagesUrl", "http://example.com/" }
            })
            .Build();

        var resolver = new ProductUrlResolver(configuration);
        var source = new Product
        {
            Manufacturer = new ProductManufacturer("Test"),
            ProductType = new ProductType("Test"),
            MainImagesNames = new List<string> { "image1.jpg", "image2.jpg" }
        };
        var destination = new FullProductDto();

        // Act
        var result = resolver.Resolve(source, destination, null, null).ToList();

        // Assert
        Assert.Collection(result,
            url => Assert.Equal
                ($"http://example.com/{source.ProductType.Name.ToLower()}s/{source.Manufacturer.Name.ToLower()}/" +
                 $"{source.ProductCode.ToLower()}/image1.jpg", url),
            url => Assert.Equal
            ($"http://example.com/{source.ProductType.Name.ToLower()}s/{source.Manufacturer.Name.ToLower()}/" +
                               $"{source.ProductCode.ToLower()}/image2.jpg", url));
    }

    [Fact]
    public void Resolve_ReturnsMainImageUrls_WhenDestinationIsGeneralizedProductDto()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ApiImagesUrl", "http://example.com/" }
            })
            .Build();

        var resolver = new ProductUrlResolver(configuration);
        var source = new Product
        {
            Manufacturer = new ProductManufacturer("Test"),
            ProductType = new ProductType("Test"),
            MainImagesNames = new List<string> { "image1.jpg", "image2.jpg" }
        };
        var destination = new GeneralizedProductDto();

        // Act
        var result = resolver.Resolve(source, destination, null, null).ToList();

        // Assert
        Assert.Collection(result,
            url => Assert.Equal($"http://example.com/{source.ProductType.Name.ToLower()}s/{source.Manufacturer.Name.ToLower()}/" +
                                $"{source.ProductCode.ToLower()}/image1.jpg", url));
    }
}