using API.Helpers.Resolvers;
using Core.Entities.Product;
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
        
        var categories = new ProductSpecificationCategoryRepositoryFactory().Create(_context);

        var cat = new List<ProductSpecificationCategory>
        {
            new("General"), new("Processor"), new("Graphics card"),
            new("Storage"), new("Random access memory"), new("Interfaces and connection"), new("Measurements")
        };

        await categories.AddNewRangeOfEntitiesAsync(cat);

        var values = new ProductSpecificationValueRepositoryFactory().Create(_context);

        var val = new List<ProductSpecificationValue>
        {
            new("For business"), new("Mac OS"), new("Apple"), new("M2 CPU Series"),
            new("M2 Ultra"), new("24"), new("2.4 GHz"), new("3.5 GHz"),
            new("5 nm"), new("Integrated"), new("M2 GPU Series"), new("M2 Ultra GPU"),
            new("Dynamic"), new("SSD"), new("PCI-ex SSD"), new("1 TB"),
            new("LPDDR5"), new("64 GB"), new("Ethernet (RJ-45), Wi-Fi 6E, Bluetooth 5.3"),
            new("Audio Line in, HDMi, USB 3.1, Thunderbolt 4"), new("197 mm"), new("95 mm"), new("2.7 kg")
        };

        await values.AddNewRangeOfEntitiesAsync(val);

        var attributes =
            new ProductSpecificationAttributeRepositoryFactory().Create(_context);

        var att = new List<ProductSpecificationAttribute>
        {
            new("Classification"), new("Operating system"), new("Manufacturer"),
            new("Series"), new("Model"), new("Quantity of cores"),
            new("Quantity of threads"), new("Base clock"), new("Max clock"),
            new("Processor technology"), new("Type"), new("Memory bus"),
            new("Type of memory"), new("Amount of memory"), new("Drive's interface"),
            new("Network adapters"), new("Connectors and I/O ports"), new("Width"),
            new("Depth"), new("Height"), new("Weight")
        };

        await attributes.AddNewRangeOfEntitiesAsync(att);

        var specs = new ProductSpecificationRepositoryFactory().Create(_context);

        var personalComputerSpecs = new List<ProductSpecification>
        {
            new(cat[0].Id, att[0].Id, val[0].Id, item.Id),
            new(cat[0].Id, att[1].Id, val[1].Id, item.Id),

            new(cat[1].Id, att[2].Id, val[2].Id, item.Id),
            new(cat[1].Id, att[3].Id, val[3].Id, item.Id),
            new(cat[1].Id, att[4].Id, val[4].Id, item.Id),
            new(cat[1].Id, att[5].Id, val[5].Id, item.Id),
            new(cat[1].Id, att[6].Id, val[5].Id, item.Id),
            new(cat[1].Id, att[7].Id, val[6].Id, item.Id),
            new(cat[1].Id, att[8].Id, val[7].Id, item.Id),
            new(cat[1].Id, att[9].Id, val[8].Id, item.Id),

            new(cat[2].Id, att[10].Id, val[9].Id, item.Id),
            new(cat[2].Id, att[2].Id, val[2].Id, item.Id),
            new(cat[2].Id, att[3].Id, val[10].Id, item.Id),
            new(cat[2].Id, att[4].Id, val[11].Id, item.Id),
            new(cat[2].Id, att[11].Id, val[12].Id, item.Id),
            new(cat[2].Id, att[12].Id, val[12].Id, item.Id),
            new(cat[2].Id, att[13].Id, val[12].Id, item.Id),

            new(cat[3].Id, att[10].Id, val[13].Id, item.Id),
            new(cat[3].Id, att[14].Id, val[14].Id, item.Id),
            new(cat[3].Id, att[13].Id, val[15].Id, item.Id),

            new(cat[4].Id, att[10].Id, val[16].Id, item.Id),
            new(cat[4].Id, att[13].Id, val[17].Id, item.Id),

            new(cat[5].Id, att[15].Id, val[18].Id, item.Id),
            new(cat[5].Id, att[16].Id, val[19].Id, item.Id),

            new(cat[6].Id, att[17].Id, val[20].Id, item.Id),
            new(cat[6].Id, att[18].Id, val[20].Id, item.Id),
            new(cat[6].Id, att[19].Id, val[21].Id, item.Id),
            new(cat[6].Id, att[20].Id, val[22].Id, item.Id)
        };
        
        await specs.AddNewRangeOfEntitiesAsync(personalComputerSpecs);
        
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
        
        var categories = new ProductSpecificationCategoryRepositoryFactory().Create(_context);

        var cat = new List<ProductSpecificationCategory>
        {
            new("General"), new("Processor"), new("Graphics card"),
            new("Storage"), new("Random access memory"), new("Interfaces and connection"), new("Measurements"),
            new("Display")
        };

        await categories.AddNewRangeOfEntitiesAsync(cat);

        var values = new ProductSpecificationValueRepositoryFactory().Create(_context);

        var val = new List<ProductSpecificationValue>
        {
            new("For business"), new("Mac OS"), new("Apple"), new("M2 CPU Series"),
            new("M2 Ultra"), new("24"), new("2.4 GHz"), new("3.5 GHz"),
            new("5 nm"), new("Integrated"), new("M2 GPU Series"), new("M2 Ultra GPU"),
            new("Dynamic"), new("SSD"), new("PCI-ex SSD"), new("1 TB"),
            new("LPDDR5"), new("64 GB"), new("Ethernet (RJ-45), Wi-Fi 6E, Bluetooth 5.3"),
            new("Audio Line in, HDMi, USB 3.1, Thunderbolt 4"), new("197 mm"), new("95 mm"), new("2.7 kg"),
            new("15.3\"")
        };

        await values.AddNewRangeOfEntitiesAsync(val);

        var attributes =
            new ProductSpecificationAttributeRepositoryFactory().Create(_context);

        var att = new List<ProductSpecificationAttribute>
        {
            new("Classification"), new("Operating system"), new("Manufacturer"),
            new("Series"), new("Model"), new("Quantity of cores"),
            new("Quantity of threads"), new("Base clock"), new("Max clock"),
            new("Processor technology"), new("Type"), new("Memory bus"),
            new("Type of memory"), new("Amount of memory"), new("Drive's interface"),
            new("Network adapters"), new("Connectors and I/O ports"), new("Width"),
            new("Depth"), new("Height"), new("Weight"), new("Diagonal")
        };

        await attributes.AddNewRangeOfEntitiesAsync(att);

        var specs = new ProductSpecificationRepositoryFactory().Create(_context);

        var personalComputerSpecs = new List<ProductSpecification>
        {
            new(cat[0].Id, att[0].Id, val[0].Id, item.Id),
            new(cat[0].Id, att[1].Id, val[1].Id, item.Id),

            new(cat[1].Id, att[2].Id, val[2].Id, item.Id),
            new(cat[1].Id, att[3].Id, val[3].Id, item.Id),
            new(cat[1].Id, att[4].Id, val[4].Id, item.Id),
            new(cat[1].Id, att[5].Id, val[5].Id, item.Id),
            new(cat[1].Id, att[6].Id, val[5].Id, item.Id),
            new(cat[1].Id, att[7].Id, val[6].Id, item.Id),
            new(cat[1].Id, att[8].Id, val[7].Id, item.Id),
            new(cat[1].Id, att[9].Id, val[8].Id, item.Id),

            new(cat[2].Id, att[10].Id, val[9].Id, item.Id),
            new(cat[2].Id, att[2].Id, val[2].Id, item.Id),
            new(cat[2].Id, att[3].Id, val[10].Id, item.Id),
            new(cat[2].Id, att[4].Id, val[11].Id, item.Id),
            new(cat[2].Id, att[11].Id, val[12].Id, item.Id),
            new(cat[2].Id, att[12].Id, val[12].Id, item.Id),
            new(cat[2].Id, att[13].Id, val[12].Id, item.Id),

            new(cat[3].Id, att[10].Id, val[13].Id, item.Id),
            new(cat[3].Id, att[14].Id, val[14].Id, item.Id),
            new(cat[3].Id, att[13].Id, val[15].Id, item.Id),

            new(cat[4].Id, att[10].Id, val[16].Id, item.Id),
            new(cat[4].Id, att[13].Id, val[17].Id, item.Id),

            new(cat[5].Id, att[15].Id, val[18].Id, item.Id),
            new(cat[5].Id, att[16].Id, val[19].Id, item.Id),

            new(cat[6].Id, att[17].Id, val[20].Id, item.Id),
            new(cat[6].Id, att[18].Id, val[20].Id, item.Id),
            new(cat[6].Id, att[19].Id, val[21].Id, item.Id),
            new(cat[6].Id, att[20].Id, val[22].Id, item.Id),
            
            new(cat[7].Id, att[21].Id, val[23].Id, item.Id)
        };
        
        await specs.AddNewRangeOfEntitiesAsync(personalComputerSpecs);
        
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
        
        var categories = new ProductSpecificationCategoryRepositoryFactory().Create(_context);

        var cat = new List<ProductSpecificationCategory>
        {
            new("General"), new("Processor"), new("Graphics card"),
            new("Storage"), new("Random access memory"), new("Interfaces and connection"), new("Measurements"),
            new("Display")
        };

        await categories.AddNewRangeOfEntitiesAsync(cat);

        var values = new ProductSpecificationValueRepositoryFactory().Create(_context);

        var val = new List<ProductSpecificationValue>
        {
            new("For business"), new("Mac OS"), new("Apple"), new("M2 CPU Series"),
            new("M2 Ultra"), new("24"), new("2.4 GHz"), new("3.5 GHz"),
            new("5 nm"), new("Integrated"), new("M2 GPU Series"), new("M2 Ultra GPU"),
            new("Dynamic"), new("SSD"), new("PCI-ex SSD"), new("1 TB"),
            new("LPDDR5"), new("64 GB"), new("Ethernet (RJ-45), Wi-Fi 6E, Bluetooth 5.3"),
            new("Audio Line in, HDMi, USB 3.1, Thunderbolt 4"), new("197 mm"), new("95 mm"), new("2.7 kg"),
            new("15.3\"")
        };

        await values.AddNewRangeOfEntitiesAsync(val);

        var attributes =
            new ProductSpecificationAttributeRepositoryFactory().Create(_context);

        var att = new List<ProductSpecificationAttribute>
        {
            new("Classification"), new("Operating system"), new("Manufacturer"),
            new("Series"), new("Model"), new("Quantity of cores"),
            new("Quantity of threads"), new("Base clock"), new("Max clock"),
            new("Processor technology"), new("Type"), new("Memory bus"),
            new("Type of memory"), new("Amount of memory"), new("Drive's interface"),
            new("Network adapters"), new("Connectors and I/O ports"), new("Width"),
            new("Depth"), new("Height"), new("Weight"), new("Diagonal")
        };

        await attributes.AddNewRangeOfEntitiesAsync(att);

        var specs = new ProductSpecificationRepositoryFactory().Create(_context);

        var personalComputerSpecs = new List<ProductSpecification>
        {
            new(cat[0].Id, att[0].Id, val[0].Id, item.Id),
            new(cat[0].Id, att[1].Id, val[1].Id, item.Id),

            new(cat[1].Id, att[2].Id, val[2].Id, item.Id),
            new(cat[1].Id, att[3].Id, val[3].Id, item.Id),
            new(cat[1].Id, att[4].Id, val[4].Id, item.Id),
            new(cat[1].Id, att[5].Id, val[5].Id, item.Id),
            new(cat[1].Id, att[6].Id, val[5].Id, item.Id),
            new(cat[1].Id, att[7].Id, val[6].Id, item.Id),
            new(cat[1].Id, att[8].Id, val[7].Id, item.Id),
            new(cat[1].Id, att[9].Id, val[8].Id, item.Id),

            new(cat[2].Id, att[10].Id, val[9].Id, item.Id),
            new(cat[2].Id, att[2].Id, val[2].Id, item.Id),
            new(cat[2].Id, att[3].Id, val[10].Id, item.Id),
            new(cat[2].Id, att[4].Id, val[11].Id, item.Id),
            new(cat[2].Id, att[11].Id, val[12].Id, item.Id),
            new(cat[2].Id, att[12].Id, val[12].Id, item.Id),
            new(cat[2].Id, att[13].Id, val[12].Id, item.Id),

            new(cat[3].Id, att[10].Id, val[13].Id, item.Id),
            new(cat[3].Id, att[14].Id, val[14].Id, item.Id),
            new(cat[3].Id, att[13].Id, val[15].Id, item.Id),

            new(cat[4].Id, att[10].Id, val[16].Id, item.Id),
            new(cat[4].Id, att[13].Id, val[17].Id, item.Id),

            new(cat[5].Id, att[15].Id, val[18].Id, item.Id),
            new(cat[5].Id, att[16].Id, val[19].Id, item.Id),

            new(cat[6].Id, att[17].Id, val[20].Id, item.Id),
            new(cat[6].Id, att[18].Id, val[20].Id, item.Id),
            new(cat[6].Id, att[19].Id, val[21].Id, item.Id),
            new(cat[6].Id, att[20].Id, val[22].Id, item.Id),
            
            new(cat[7].Id, att[21].Id, val[23].Id, item.Id)
        };
        
        await specs.AddNewRangeOfEntitiesAsync(personalComputerSpecs);
        
        var resolver = new ProductShortDescriptionResolver();

        var result = resolver.Resolve(item, null, null, null);

        Assert.NotNull(result);
        Assert.IsType<string>(result);
        Assert.Equal("Display: 15.3\" | CPU: M2 Ultra | GPU: M2 Ultra GPU | RAM: 64 GB | ROM: 1 TB | OS: Mac OS",
            result);
    }
    
    [Fact]
    public void Resolve_Should_Throw_ArgumentException_If_UnknownProductWasPassed()
    {
        var item = new Product()
        {
            ProductType = new ProductType("Unknown type")
        };
        
        var resolver = new ProductShortDescriptionResolver();

        Assert.Throws<ArgumentException>(() => resolver.Resolve(item, null, null, null));
    }
    
    private static Product GetFullyInitializedProduct
        (ProductManufacturer manufacturer, ProductRating rating, ProductType type) => 
        new(
        "Test",
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