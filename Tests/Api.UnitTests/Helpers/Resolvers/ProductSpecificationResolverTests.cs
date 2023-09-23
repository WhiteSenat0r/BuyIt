using API.Helpers.DataTransferObjects.ProductRelated;
using API.Helpers.Resolvers;
using Core.Entities.Product;
using Core.Entities.Product.ProductSpecificationRelated;
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
        
        var categories = new ProductSpecificationCategoryRepositoryFactory().Create(_context);

        var cat = new List<ProductSpecificationCategory>
        {
            new("General")
        };

        await categories.AddNewRangeOfEntitiesAsync(cat);

        var values = new ProductSpecificationValueRepositoryFactory().Create(_context);

        var val = new List<ProductSpecificationValue>
        {
            new("For business"), new("Mac OS")
        };

        await values.AddNewRangeOfEntitiesAsync(val);

        var attributes =
            new ProductSpecificationAttributeRepositoryFactory().Create(_context);

        var att = new List<ProductSpecificationAttribute>
        {
            new("Classification"), new("Operating system")
        };

        await attributes.AddNewRangeOfEntitiesAsync(att);

        var personalComputerSpecs = new List<ProductSpecification>
        {
            new(cat[0].Id, att[0].Id, val[0].Id, item.Id),
            new(cat[0].Id, att[1].Id, val[1].Id, item.Id),
        };
        
        var specsRepo = new ProductSpecificationRepositoryFactory().Create(_context);
        
        await specsRepo.AddNewRangeOfEntitiesAsync(personalComputerSpecs);
        
        var resolver = new ProductSpecificationResolver();

        var destination = new FullProductDto();

        var result = resolver.Resolve(item, destination, null, null);

        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
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