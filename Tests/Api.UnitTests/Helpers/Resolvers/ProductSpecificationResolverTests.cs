using API.Helpers.DataTransferObjects.ProductRelated;
using API.Helpers.Resolvers;
using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.Factories.ProductRelated;
using Microsoft.EntityFrameworkCore;

namespace Tests.Api.UnitTests.Helpers.Resolvers;

public class ProductProductSpecificationResolverTests
{
    private StoreContext _context = null!;
    private IRepository<Product> _repository = null!;
    
    [Fact]
    public async void Resolve_ReturnsCorrectProductSpecificationDictionary()
    {
        _context = new StoreContext(new DbContextOptionsBuilder<StoreContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        var manufacturerRepo = new ProductManufacturerRepositoryFactory().Create(_context);
        var manufacturer = new ProductManufacturer("TestManufacturer", "TestCountry");
        await manufacturerRepo.AddNewEntityAsync(manufacturer);
        
        var ratingRepo = new ProductRatingRepositoryFactory().Create(_context);
        var rating = new ProductRating(null);
        await ratingRepo.AddNewEntityAsync(rating);

        var typeRepo = new ProductTypeRepositoryFactory().Create(_context);
        var type = new ProductType("Test");
        await typeRepo.AddNewEntityAsync(type);
        
        _repository = new ProductRepositoryFactory().Create(_context);

        var item = new Product
            ("Test", "Test", 1, true, manufacturer, type, rating, new [] { "1.jpg"});
        
        await _repository.AddNewEntityAsync(item);
        
        var specs = new List<ProductSpecification>
        {
            new() { Category = "Color", Attribute = "Color", Value = "Red", ProductId = item.Id},
            new() { Category = "Color", Attribute = "Material", Value = "Cotton", ProductId = item.Id },
            new() { Category = "Size", Attribute = "Size", Value = "Large", ProductId = item.Id },
        };
        
        var specsRepo = new ProductSpecificationRepositoryFactory().Create(_context);
        
        await specsRepo.AddNewRangeOfEntitiesAsync(specs);
        
        var resolver = new ProductSpecificationResolver();

        var destination = new FullProductDto();

        var result = resolver.Resolve(item, destination, null, null);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        Assert.True(result.ContainsKey("Color"));
        var colorAttributes = result["Color"];
        Assert.Equal("Red", colorAttributes["Color"]);
        Assert.Equal("Cotton", colorAttributes["Material"]);

        Assert.True(result.ContainsKey("Size"));
        var sizeAttributes = result["Size"];
        Assert.Equal("Large", sizeAttributes["Size"]);
    }

    [Fact]
    public async void Resolve_ReturnsEmptyDictionary_WhenProductSpecificationsAreEmpty()
    {
        _context = new StoreContext(new DbContextOptionsBuilder<StoreContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        var manufacturerRepo = new ProductManufacturerRepositoryFactory().Create(_context);
        var manufacturer = new ProductManufacturer("TestManufacturer", "TestCountry");
        await manufacturerRepo.AddNewEntityAsync(manufacturer);
        
        var ratingRepo = new ProductRatingRepositoryFactory().Create(_context);
        var rating = new ProductRating(null);
        await ratingRepo.AddNewEntityAsync(rating);

        var typeRepo = new ProductTypeRepositoryFactory().Create(_context);
        var type = new ProductType("Test");
        await typeRepo.AddNewEntityAsync(type);
        
        _repository = new ProductRepositoryFactory().Create(_context);

        var item = new Product
            ("Test", "Test", 1, true, manufacturer, type, rating, new [] { "1.jpg" });
        
        await _repository.AddNewEntityAsync(item);

        var destination = new FullProductDto();
    
        var resolver = new ProductSpecificationResolver();

        Assert.Throws<ArgumentNullException>(
            () => resolver.Resolve(item, destination, null, null));
    }
}