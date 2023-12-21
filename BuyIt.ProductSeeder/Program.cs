using Application.Specifications.ProductManufacturerSpecifications;
using Application.Specifications.ProductSpecifications;
using Application.Specifications.ProductTypeSpecifications;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Factories.ProductRelated;
using Persistence.Repositories.ProductRelatedRepositories;

namespace BuyIt.ProductSeeder;

class Program
{
    private static IRepository<Product> _productsRepository;
    private static IRepository<ProductManufacturer> _brandsRepository;
    private static IRepository<ProductType> _categoriesRepository;
    private static IRepository<ProductRating> _ratingsRepository;
    private static ProductSpecificationRepository _specsRepository;
    
    static async Task Main()
    {
        InitializeRepositories();
        
        var productSpecifications = GetProductSpecifications();
        
        var specList = new List<ProductSpecification>();
        
        await AddSpecsForProductInitializationAsync(productSpecifications, specList);
        
        var type = await GetCategoryByNameAsync("Personal computer");
        
        var rating = new ProductRating(null);
        
        await _ratingsRepository.AddNewEntityAsync(rating);
        
        var brand = await GetBrandByNameAsync("ARTLINE");
        
        var newItem = InitializeNewProduct(brand, type, rating, specList);
        
        await _productsRepository.AddNewEntityAsync(newItem);
        
        Console.WriteLine("OK!");
    }

    private static Product InitializeNewProduct(
        ProductManufacturer brand, ProductType type,
        ProductRating rating, List<ProductSpecification> specList)
    {
        return new Product(
            "ARTLINE Gaming X75 (X75v22)",
            "ARTLINE Gaming X75 is a representative of a series of gaming PCs that attracts with its interesting design and thoughtful build. It is suitable for those who are looking for an advanced gaming platform, a work PC with \"upgraded\" graphics capabilities, a multimedia center for entertainment.\n\nIt has a distinctive gaming style and a transparent side panel, which makes it the centerpiece of a gaming room and a beautiful decorative addition to the interior.\n\nThe power of this PC meets the needs of enthusiasts and gamers. It has an Intel Core i7-11700F central processor, 32 GB of RAM, and a discrete graphics card. With this hardware configuration, everyone can feel like a winner when playing against strong opponents. To store a collection of games or other multimedia files, a hybrid drive is provided that combines the advantages of different types of disks.\n\nThe personal computer is equipped with all the necessary ports for convenient and fast connection of peripherals, accessories, and drives. A powerful 700W power supply unit provides efficient power to the internal components.",
            1049.99m,
            true,
            brand,
            type,
            rating,
            new [] { "1.jpg", "2.jpg", "3.jpg" })
        {
            Specifications = specList
        };
    }

    private static async Task<ProductManufacturer> GetBrandByNameAsync(string brand) => 
        await _brandsRepository.GetSingleEntityBySpecificationAsync(
            new ProductManufacturerQueryByNameSpecification(brand));

    private static async Task<ProductType> GetCategoryByNameAsync(string category) =>
        await _categoriesRepository.GetSingleEntityBySpecificationAsync(
            new ProductTypeQueryByNameSpecification(category));

    private static async Task AddSpecsForProductInitializationAsync(
        Dictionary<string, Dictionary<string, string>> productSpecifications,
        ICollection<ProductSpecification> specList)
    {
        foreach (var spec in productSpecifications)
        {
            foreach (var attribute in spec.Value)
            {
                await _specsRepository.TryAddNewSpecificationAsync(
                    spec.Key, attribute.Key, attribute.Value);
        
                specList.Add(await _specsRepository.GetSingleEntityBySpecificationAsync(
                    new ProductSpecificationQuerySpecification(
                        s => s.SpecificationCategory.Value.Equals(spec.Key) &&
                             s.SpecificationAttribute.Value.Equals(attribute.Key) &&
                             s.SpecificationValue.Value.Equals(attribute.Value))));
            }
        }
    }

    private static Dictionary<string, Dictionary<string, string>> GetProductSpecifications()
    {
        return new Dictionary<string, Dictionary<string, string>>
        {
            {
                "General", new Dictionary<string, string>
                {
                    { "Operating system", "Without OS" },
                    { "Classification", "For gaming" },
                }
            },
            {
                "Processor", new Dictionary<string, string>
                {
                    { "Quantity of cores", "8" },
                    { "Model", "i7-11700F" },
                    { "Quantity of threads", "16" },
                    { "Base clock", "2.5 GHz" },
                    { "Processor technology", "14 nm" },
                    { "Manufacturer", "Intel" },
                    { "Max clock", "4.9 GHz" },
                    { "Series", "Core i7" },
                }
            },
            {
                "Graphics card", new Dictionary<string, string>
                {
                    { "Type of memory", "GDDR6" },
                    { "Model", "GeForce RTX 3060" },
                    { "Amount of memory", "12 GB" },
                    { "Memory bus", "192 bit" },
                    { "Manufacturer", "Nvidia" },
                    { "Series", "GeForce RTX 30-Series" },
                    { "Type", "Discrete" },
                }
            },
            {
                "Random access memory", new Dictionary<string, string>
                {
                    { "Type", "DDR4" },
                    { "Amount of memory", "16 GB" },
                }
            },
            {
                "Storage", new Dictionary<string, string>
                {
                    { "Type", "SSD" },
                    { "Drive's interface", "SATA 3 SSD" },
                    { "Amount of memory", "1 TB" },
                }
            },
            {
                "Interfaces and connection", new Dictionary<string, string>
                {
                    { "Network adapters", "Ethernet (RJ-45)" },
                    { "Connectors and I/O ports", "Audio Line in, Audio Line out, DisplayPort, HDMi, PS/2, USB 2.0, USB 3.0" }
                }
            },
            {
                "Measurements", new Dictionary<string, string>
                {
                    { "Weight", "10 kg" },
                    { "Depth", "420 mm" },
                    { "Height", "450 mm" },
                    { "Width", "192 mm" },
                }
            },
        };
    }

    static void InitializeRepositories()
    {
        var dbContext = new StoreContext(
            new DbContextOptionsBuilder<StoreContext>()
                .UseSqlServer("Server=localhost;Database=BuyItDB;Trusted_Connection=True;" +
                              "TrustServerCertificate=True;")
                .Options);
        
        _productsRepository = new ProductRepositoryFactory().Create(dbContext);
        _ratingsRepository = new ProductRatingRepositoryFactory().Create(dbContext);
        _categoriesRepository = new ProductTypeRepositoryFactory().Create(dbContext);
        _brandsRepository = new ProductManufacturerRepositoryFactory().Create(dbContext);
        _specsRepository = new ProductSpecificationRepositoryFactory().Create(dbContext);
    }
}