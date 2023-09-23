using Core.Entities.Product;
using Core.Entities.Product.ProductSpecificationRelated;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.Factories.ProductRelated;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductRatingQueries;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.UnitTests.RepositoryRelatedTests.ProductRelatedRepositoryTests;

public class ProductRepositoryTests
{
    private IRepository<Product> _repository = null!;
    private StoreContext _context = null!;

    [Fact]
    public async void RemoveExistingEntityMethod_Should_RemoveSingleProductWithRatingAndManufacturerFromDatabase()
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
        var type = new ProductType("Personal computer");
        await typeRepo.AddNewEntityAsync(type);
        
        _repository = new ProductRepositoryFactory().Create(_context);

        await _repository.AddNewEntityAsync(GetFullyInitializedProduct(manufacturer, rating, type));
        
        _repository.RemoveExistingEntity(await _repository.GetSingleEntityBySpecificationAsync
            (new ProductQuerySpecification()));
        
        Assert.Empty(await _repository.GetAllEntitiesAsync(new ProductQuerySpecification()));
        Assert.Empty(await manufacturerRepo.GetAllEntitiesAsync(new ProductManufacturerQuerySpecification()));
        Assert.Empty(await ratingRepo.GetAllEntitiesAsync(new ProductRatingQuerySpecification()));
    }

    [Fact]
    public async void RemoveExistingEntityMethod_Should_RemoveRangeOfProductsWithRatingsAndManufacturersFromDatabase()
    {
        _context = new StoreContext(new DbContextOptionsBuilder<StoreContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        var manufacturerRepo = new ProductManufacturerRepositoryFactory().Create(_context);
        var manufacturer = new ProductManufacturer("TestManufacturer", "TestCountry");
        await manufacturerRepo.AddNewEntityAsync(manufacturer);
        
        var ratingRepo = new ProductRatingRepositoryFactory().Create(_context);
        var ratings = new List<ProductRating> { new(null), new(null) };
        await ratingRepo.AddNewRangeOfEntitiesAsync(ratings);

        var typeRepo = new ProductTypeRepositoryFactory().Create(_context);
        var type = new ProductType("Personal computer");
        await typeRepo.AddNewEntityAsync(type);
        
        _repository = new ProductRepositoryFactory().Create(_context);

        var products = new List<Product>
        {
            GetFullyInitializedProduct(manufacturer, ratings[0], type),
            GetFullyInitializedProduct(manufacturer, ratings[1], type),
        };

        var specsRepo = new ProductSpecificationRepositoryFactory().Create(_context);

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
            new(cat[0].Id, att[0].Id, val[0].Id, products[0].Id),
            new(cat[0].Id, att[1].Id, val[1].Id, products[1].Id),
        };

        await specsRepo.AddNewRangeOfEntitiesAsync(personalComputerSpecs);
        
        await _repository.AddNewRangeOfEntitiesAsync(products);
        
        _repository.RemoveRangeOfExistingEntities(products);
        
        Assert.Empty(await _repository.GetAllEntitiesAsync(new ProductQuerySpecification()));
        Assert.Empty(await manufacturerRepo.GetAllEntitiesAsync(new ProductManufacturerQuerySpecification()));
        Assert.Empty(await ratingRepo.GetAllEntitiesAsync(new ProductRatingQuerySpecification()));
    }
    
    private static Product GetFullyInitializedProduct
        (ProductManufacturer manufacturer, ProductRating rating, ProductType type) => new(
        @"Apple Mac Studio M2 Ultra 2023 (MQH63)",
        "M2 Ultra delivers the power to tackle virtually any size project. From recording your own beats or " +
        "mixing professional-quality music to editing your first video or adding effects to a feature film, the" +
        " lightning-fast M2 Ultra has your back. The 7.7-square-inch Mac Studio enclosure houses a revolutionary " +
        "thermal system designed to enable the M2 Ultra to perform intensive tasks at lightning speed. Despite this" +
        " incredible power, Mac Studio can remain quiet so it never interferes with your workflow. Mac Studio lets" +
        " you create the studio of your dreams with an array of 12 high-performance ports. Send photos and videos with" +
        " the SDXC card reader. Connect to TVs or displays with enhanced HDMI output supporting resolutions up to 8K." +
        " Synchronize with next-generation accessories with Bluetooth 5.3. And enjoy twice the bandwidth with Wi-Fi 6E." +
        " With an incredibly compact form factor and a variety of ports, Mac Studio lets you reimagine your workspace" +
        " and unleash your creativity. macOS was designed not only to be powerful, intuitive, and constantly updated, but" +
        " also to scale with Apple Silicon. That means the system automatically benefits from increased graphics, more " +
        "memory, and powerful M2 Ultra and machine learning. And with thousands of apps to choose from, you can work," +
        " play, and create in ways you never thought possible.",
        5810.99m,
        false,
        manufacturer,
        type,
        rating,
        new List<string>
        {
            "1.jpg",
            "2.jpg",
            "3.jpg",
            "4.jpg"
        });
}