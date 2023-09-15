using API.Helpers.Resolvers;
using Core.Entities.Product;
using Core.Entities.Product.Common.Interfaces;
using Core.Entities.Product.ProductSpecification;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.Factories.ProductRelated;
using Microsoft.EntityFrameworkCore;

namespace Tests.Api.UnitTests.Helpers.Resolvers;

public class ProductShortDescriptionResolverTests
{
    private StoreContext _context = null!;
    private IRepository<Product> _repository = null!;
    
    [Fact]
    public async void Resolve_Should_Return_ComputerShortDescription_For_PersonalComputer_ProductType()
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

        var item = GetFullyInitializedProduct(manufacturer, rating, type);
        
        await _repository.AddNewEntityAsync(item);
        
        var personalComputerSpecs = GetPersonalComputerSpecs(item);
        
        var specsRepo = new ProductSpecificationRepositoryFactory().Create(_context);
        
        await specsRepo.AddNewRangeOfEntitiesAsync(personalComputerSpecs);
        
        var resolver = new ProductShortDescriptionResolver();

        var result = resolver.Resolve(item, null, null, null);

        Assert.NotNull(result);
        Assert.IsType<string>(result);
        Assert.Equal("CPU: M2 Ultra | GPU: M2 Ultra GPU | RAM: 64 GB | ROM: 1 TB", result);
    }

    [Fact]
    public async void Resolve_Should_Return_LaptopShortDescription_For_Laptop_ProductType()
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
        var type = new ProductType("Laptop");
        await typeRepo.AddNewEntityAsync(type);
        
        _repository = new ProductRepositoryFactory().Create(_context);

        var item = GetFullyInitializedProduct(manufacturer, rating, type);
        
        await _repository.AddNewEntityAsync(item);
        
        var personalComputerSpecs = GetExtendedComputerSpecs(item);
        
        var specsRepo = new ProductSpecificationRepositoryFactory().Create(_context);
        
        await specsRepo.AddNewRangeOfEntitiesAsync(personalComputerSpecs);
        
        var resolver = new ProductShortDescriptionResolver();

        var result = resolver.Resolve(item, null, null, null);

        Assert.NotNull(result);
        Assert.IsType<string>(result);
        Assert.Equal("Display: 15.3\" | CPU: M2 Ultra | GPU: M2 Ultra GPU | RAM: 64 GB | ROM: 1 TB", result);
    }
    
    [Fact]
    public async void Resolve_Should_Return_AllInOneComputerShortDescription_For_AllInOneComputer_ProductType()
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
        var type = new ProductType("All-in-one computer");
        await typeRepo.AddNewEntityAsync(type);
        
        _repository = new ProductRepositoryFactory().Create(_context);

        var item = GetFullyInitializedProduct(manufacturer, rating, type);
        
        await _repository.AddNewEntityAsync(item);
        
        var personalComputerSpecs = GetExtendedComputerSpecs(item);
        
        var specsRepo = new ProductSpecificationRepositoryFactory().Create(_context);
        
        await specsRepo.AddNewRangeOfEntitiesAsync(personalComputerSpecs);
        
        var resolver = new ProductShortDescriptionResolver();

        var result = resolver.Resolve(item, null, null, null);

        Assert.NotNull(result);
        Assert.IsType<string>(result);
        Assert.Equal("Display: 15.3\" | CPU: M2 Ultra | GPU: M2 Ultra GPU | RAM: 64 GB | ROM: 1 TB | OS: Mac OS",
            result);
    }
    
    [Fact]
    public async void Resolve_Should_Throw_ArgumentException_If_UnknownProductWasPassed()
    {
        var item = new Product()
        {
            ProductType = new ProductType("Unknown type")
        };
        
        var resolver = new ProductShortDescriptionResolver();

        Assert.Throws<ArgumentException>(() => resolver.Resolve(item, null, null, null));
    }
    
    private static IEnumerable<ProductSpecification> GetPersonalComputerSpecs(IProduct item)
    {
        return new List<ProductSpecification> 
        { 
            new ("General", "Classification", "For business", item.Id),
            new ("General", "Operating system", "Mac OS", item.Id),

            new ("Processor", "Manufacturer", "Apple", item.Id),
            new ("Processor", "Series", "M2 CPU Series", item.Id),
            new ("Processor", "Model", "M2 Ultra", item.Id),
            new ("Processor", "Quantity of cores", "24", item.Id),
            new ("Processor", "Quantity of threads", "24", item.Id),
            new ("Processor", "Base clock", "2.4 GHz", item.Id),
            new ("Processor", "Max clock", "3.5 GHz", item.Id),
            new ("Processor", "Processor technology", "5 nm", item.Id),

            new ("Graphics card","Type","Integrated" ,item.Id),
            new ("Graphics card","Manufacturer","Apple" ,item.Id),
            new ("Graphics card", "Series", "M2 GPU Series", item.Id),
            new ("Graphics card","Model","M2 Ultra GPU" ,item.Id),
            new ("Graphics card","Memory bus","Dynamic" ,item.Id),
            new ("Graphics card","Type of memory","Dynamic" ,item.Id),
            new ("Graphics card","Amount of memory","Dynamic" ,item.Id),

            new ("Storage","Type","SSD" ,item.Id),
            new ("Storage","Drive's interface","PCI-ex SSD" ,item.Id),
            new ("Storage","Amount of memory","1 TB" ,item.Id),

            new ("Random access memory","Type","LPDDR5" ,item.Id),
            new ("Random access memory","Amount of memory","64 GB" ,item.Id),
    
            new ("Interfaces and connection","Network adapters","Ethernet (RJ-45), Wi-Fi 6E, Bluetooth 5.3" ,item.Id),
            new ("Interfaces and connection","Connectors and I/O ports",
                "Audio Line in, HDMi, USB 3.1, Thunderbolt 4" ,item.Id),
    
            new ("Measurements","Width","197 mm" ,item.Id),
            new ("Measurements","Depth","197 mm" ,item.Id),
            new ("Measurements","Height","95 mm" ,item.Id),
            new ("Measurements","Weight","2.7 kg" ,item.Id)
        };
    }
    
    private static IEnumerable<ProductSpecification> GetExtendedComputerSpecs(IProduct item)
    {
        return GetPersonalComputerSpecs(item).Append
            (new ("Display", "Diagonal", "15.3\"", item.Id));
    }
    
    private static Product GetFullyInitializedProduct
        (ProductManufacturer manufacturer, ProductRating rating, ProductType type) => 
        new(
        @"Test",
        "Test",
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