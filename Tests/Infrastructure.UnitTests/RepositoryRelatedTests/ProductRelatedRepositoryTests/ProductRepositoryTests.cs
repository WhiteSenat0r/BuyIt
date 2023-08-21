using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.Factories.ProductRelated;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries;
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
        var type = new ProductType("Laptop");
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
        var ratings = new List<ProductRating> { new ProductRating(null), new ProductRating(null) };
        await ratingRepo.AddNewRangeOfEntitiesAsync(ratings);

        var typeRepo = new ProductTypeRepositoryFactory().Create(_context);
        var type = new ProductType("Laptop");
        await typeRepo.AddNewEntityAsync(type);
        
        _repository = new ProductRepositoryFactory().Create(_context);

        var products = new List<Product>
        {
            GetFullyInitializedProduct(manufacturer, ratings[0], type),
            GetFullyInitializedProduct(manufacturer, ratings[1], type),
        };
        
        await _repository.AddNewRangeOfEntitiesAsync(products);
        
        _repository.RemoveRangeOfExistingEntities(products);
        
        Assert.Empty(await _repository.GetAllEntitiesAsync(new ProductQuerySpecification()));
        Assert.Empty(await manufacturerRepo.GetAllEntitiesAsync(new ProductManufacturerQuerySpecification()));
        Assert.Empty(await ratingRepo.GetAllEntitiesAsync(new ProductRatingQuerySpecification()));
    }
    
    private Product GetFullyInitializedProduct
        (ProductManufacturer manufacturer, ProductRating rating, ProductType type) => new(
        @"ASUS Zenbook 14X OLED UX5401ZA-L7065X Pine Grey",
        "Zenbook 14X UX540 is a modern solution for creatives. " +
        "Weighing just 1.4 kg and 16 mm thick, it's portable and fits comfortably in any bag. " +
        "With a high-res screen, wide color range, and proprietary tech, it's perfect for content editing " +
        "and movie-watching. The slim bezel enhances immersion, and the 180° hinge adds versatility. Powered" +
        " by the latest Intel Core i9-12900H processor, it ensures efficient work with ample memory. " +
        "The ClearVoice Mic function makes it ideal for remote work and video calls. Its sleek design suits " +
        "those on the move, offering both style and performance. Zenbook 14X is an artful choice for success-driven" +
        " individuals.",
        2115.99m,
        true,
        manufacturer,
        type,
        rating,
        new List<string>()
        {
            "https://i.imgur.com/t8nTGmY.jpg",
            "https://i.imgur.com/p69Qtwy.jpg",
            "https://i.imgur.com/8KBW5Ax.jpg",
            "https://i.imgur.com/zXa5aZu.jpg",
            "https://i.imgur.com/QBPblx7.jpg"
        },
        null,
        new Dictionary<string, IDictionary<string, string>>()
        {
            {
                "Processor", new Dictionary<string, string>()
                {
                    {
                        "Manufacturer", "Intel"
                    },
                    {
                        "Series", "Intel Core i9"
                    },
                    {
                        "Model", "12900H"
                    },
                    {
                        "Quantity of cores", "14"
                    },
                    {
                        "Quantity of threads", "28"
                    },
                    {
                        "Base clock", "3.8 Ghz"
                    },
                    {
                        "Max clock", "5.0 Ghz"
                    },
                    {
                        "Processor technology", "Intel 7"
                    }
                }
            },
            {
                "General", new Dictionary<string, string>()
                {
                    {
                        "Classification", "Premium"
                    },
                    {
                        "Model family", "ASUS ZenBook"
                    },
                    {
                        "Operating system", "Windows 11 Professional"
                    }
                }
            },
            {
                "Graphics card", new Dictionary<string, string>()
                {
                    {
                        "Type", "Integrated"
                    },
                    {
                        "Manufacturer", "Intel"
                    },
                    {
                        "Model", "Intel Iris Xe Graphics G7 96EU"
                    },
                    {
                        "Series", "Intel Iris Xe Graphics"
                    },
                    {
                        "Memory bus", "Dynamic"
                    },
                    {
                        "Type of memory", "Dynamic"
                    },
                    {
                        "Amount of memory", "Dynamic"
                    }
                }
            },
            {
                "Storage", new Dictionary<string, string>()
                {
                    {
                        "Type", "SSD"
                    },
                    {
                        "Drive's interface", "PCI-ex SSD"
                    },
                    {
                        "Amount of memory", "1 TB"
                    }
                }
            },
            {
                "Random access memory", new Dictionary<string, string>()
                {
                    {
                        "Type", "DDR5"
                    },
                    {
                        "Amount of memory", "32 GB"
                    }
                }
            },
            {
                "Measurements", new Dictionary<string, string>()
                {
                    {
                        "Width", "311 mm"
                    },
                    {
                        "Length", "221 mm"
                    },
                    {
                        "Depth", "16 mm"
                    },
                    {
                        "Weight", "1.4 Kg"
                    }
                }
            },
            {
                "Interfaces and connection", new Dictionary<string, string>()
                {
                    {
                        "Network adapters", "Bluetooth, WiFi 802.11ax"
                    },
                    {
                        "Web-camera", "Present"
                    },
                    {
                        "Web-camera resolution", "1280x720 1.0 Mp"
                    },
                    {
                        "Built-in microphone", "Present"
                    },
                    {
                        "Built-in card reader", "Present"
                    },
                    {
                        "Supported card types", "MicroSD"
                    },
                    {
                        "Connectors and I/O ports", "Audio Line out, HDMi, Thunderbolt, USB 3.2"
                    }
                }
            },
            {
                "Display", new Dictionary<string, string>()
                {
                    {
                        "Diagonal", "14\""
                    },
                    {
                        "Resolution", "2880x1800"
                    },
                    {
                        "Coating", "Glossy"
                    },
                    {
                        "Matrix type", "OLED"
                    },
                    {
                        "Display type", "Regular"
                    },
                    {
                        "Refresh rate", "90 Hz"
                    }
                }
            },
            {
                "Battery", new Dictionary<string, string>()
                {
                    {
                        "Type", "Built-in"
                    },
                    {
                        "Capacity", "63 Watt-hours"
                    }
                }
            },
            {
                "Additional options", new Dictionary<string, string>()
                {
                    {
                        "Optical drive", "Absent"
                    },
                    {
                        "Numeric keypad", "Present"
                    },
                    {
                        "Keyboard backlight", "Present"
                    }
                }
            },
        });
}