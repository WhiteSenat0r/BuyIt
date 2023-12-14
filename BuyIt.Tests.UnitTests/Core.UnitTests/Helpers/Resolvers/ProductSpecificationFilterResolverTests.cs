using Application.FilteringModels;
using Application.Helpers.SpecificationResolver;
using Application.Specifications.ProductManufacturerSpecifications;
using Application.Specifications.ProductSpecifications;
using Application.Specifications.ProductSpecifications.ComputerRelatedSpecifications;
using Application.Specifications.ProductTypeSpecifications;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Moq;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.Helpers.Resolvers;

public class ProductSpecificationFilterResolverTests
{
    [Fact]
    public void Constructor_InitializesObjectCorrectly()
    {
        var resolver = new ProductSpecificationFilterResolver();

        Assert.NotNull(resolver);
    }
    
    [Fact]
    public async Task ResolveAsync_Method_GetsCountedSearchFilterOptionsCorrectly()
    {
        var resolver = new ProductSpecificationFilterResolver();

        var productBrands = new Mock<IRepository<ProductManufacturer>>();
        productBrands.Setup(r => r.GetAllEntitiesAsync(
                It.IsAny<ProductManufacturerQuerySpecification>()))
            .ReturnsAsync(new List<ProductManufacturer>
            {
                new("TestM")
            });
        
        var productSpecCategory = new ProductSpecificationCategory("General");
        var productSpecAttribute = new ProductSpecificationAttribute("Operating system");
        var productSpecValue = new ProductSpecificationValue("Operating system");
        
        var deletedSpecCategory = new ProductSpecificationCategory("Processor");
        var deletedSpecAttribute = new ProductSpecificationAttribute("Processor technology");
        var deletedSpecValue = new ProductSpecificationValue("Test");
        
        var productSpecRepo = new Mock<IRepository<ProductSpecification>>();
        productSpecRepo.Setup(r => r.GetAllEntitiesAsync(
            It.IsAny<ProductSpecificationQuerySpecification>())).ReturnsAsync(
            new List<ProductSpecification>
            {
                new(productSpecCategory.Id, productSpecAttribute.Id, productSpecValue.Id)
                {
                    SpecificationCategory = productSpecCategory,
                    SpecificationAttribute = productSpecAttribute,
                    SpecificationValue = productSpecValue
                },
                new(deletedSpecCategory.Id, deletedSpecAttribute.Id, deletedSpecValue.Id)
                {
                    SpecificationCategory = deletedSpecCategory,
                    SpecificationAttribute = deletedSpecAttribute,
                    SpecificationValue = deletedSpecValue
                }
            });
        
        var productCategories = new Mock<IRepository<ProductType>>();
        productCategories.Setup(r => r.GetAllEntitiesAsync(
            It.IsAny<IQuerySpecification<ProductType>>())).ReturnsAsync(
            new List<ProductType>
            {
                new("Personal computer")
            });
        
        var brand = (await productBrands.Object.GetAllEntitiesAsync(
            new ProductManufacturerQuerySpecification())).First();
        
        var category = (await productCategories.Object.GetAllEntitiesAsync(
            new ProductTypeQuerySpecification())).First();
        
        var productRepo = new Mock<IRepository<Product>>();
        productRepo.Setup(r => r.GetAllEntitiesAsync(
            It.IsAny<ProductSearchQuerySpecification>())).ReturnsAsync(new List<Product>
        {
            new ("Test", "Test", 1m,
                true, brand, category,
                new ProductRating(), new [] { "1.jpg", "2.jpg" } )
            {
                Specifications = await productSpecRepo.Object.GetAllEntitiesAsync(
                    new ProductSpecificationQuerySpecification()),
                Manufacturer = brand,
                ProductType = category
            },
            new ("Test", "Test", 1m,
                true, brand, category,
                new ProductRating(), new [] { "1.jpg", "2.jpg" })
            {
                Specifications = await productSpecRepo.Object.GetAllEntitiesAsync(
                    new ProductSpecificationQuerySpecification()),
                Manufacturer = brand,
                ProductType = category
            }
        });

        var searchFilteringModel = new ProductSearchFilteringModel();
        
        brand.Products = await productRepo.Object.GetAllEntitiesAsync(
            new ProductSearchQuerySpecification(searchFilteringModel));
        productBrands.Object.UpdateExistingEntity(brand);

        category.Products = await productRepo.Object.GetAllEntitiesAsync(
            new ProductSearchQuerySpecification(searchFilteringModel));
        productCategories.Object.UpdateExistingEntity(category);
        
        var result = await resolver.ResolveAsync(
            productRepo.Object, productSpecRepo.Object, productBrands.Object,
            productCategories.Object, searchFilteringModel);
        
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task ResolveAsync_Method_GetsCountedNonSearchFilterOptionsCorrectly()
    {
        var resolver = new ProductSpecificationFilterResolver();

        var productBrands = new Mock<IRepository<ProductManufacturer>>();
        productBrands.Setup(r => r.GetAllEntitiesAsync(
                It.IsAny<IQuerySpecification<ProductManufacturer>>()))
            .ReturnsAsync(new List<ProductManufacturer>
            {
                new("TestM")
            });
        
        var productSpecCategory = new ProductSpecificationCategory("General");
        var productSpecAttribute = new ProductSpecificationAttribute("Operating system");
        var productSpecValue = new ProductSpecificationValue("Operating system");
        
        var deletedSpecCategory = new ProductSpecificationCategory("Processor");
        var deletedSpecAttribute = new ProductSpecificationAttribute("Processor technology");
        var deletedSpecValue = new ProductSpecificationValue("Test");
        
        var productSpecRepo = new Mock<IRepository<ProductSpecification>>();
        productSpecRepo.Setup(r => r.GetAllEntitiesAsync(
            It.IsAny<IQuerySpecification<ProductSpecification>>())).ReturnsAsync(
            new List<ProductSpecification>
            {
                new(productSpecCategory.Id, productSpecAttribute.Id, productSpecValue.Id)
                {
                    SpecificationCategory = productSpecCategory,
                    SpecificationAttribute = productSpecAttribute,
                    SpecificationValue = productSpecValue
                },
                new(deletedSpecCategory.Id, deletedSpecAttribute.Id, deletedSpecValue.Id)
                {
                    SpecificationCategory = deletedSpecCategory,
                    SpecificationAttribute = deletedSpecAttribute,
                    SpecificationValue = deletedSpecValue
                }
            });
        
        var productCategories = new Mock<IRepository<ProductType>>();
        productCategories.Setup(r => r.GetAllEntitiesAsync(
            It.IsAny<IQuerySpecification<ProductType>>())).ReturnsAsync(
            new List<ProductType>
            {
                new("Personal computer")
            });
        
        var brand = (await productBrands.Object.GetAllEntitiesAsync(
            new ProductManufacturerQuerySpecification())).First();
        
        var category = (await productCategories.Object.GetAllEntitiesAsync(
            new ProductTypeQuerySpecification())).First();
        
        var productRepo = new Mock<IRepository<Product>>();
        productRepo.Setup(r => r.GetAllEntitiesAsync(
            It.IsAny<IQuerySpecification<Product>>())).ReturnsAsync(new List<Product>
        {
            new ("Test", "Test", 1m,
                true, brand, category,
                new ProductRating(), new [] { "1.jpg", "2.jpg" } )
            {
                Specifications = await productSpecRepo.Object.GetAllEntitiesAsync(
                    new ProductSpecificationQuerySpecification()),
                Manufacturer = brand,
                ProductType = category
            },
            new ("Test", "Test", 1m,
                true, brand, category,
                new ProductRating(), new [] { "1.jpg", "2.jpg" })
            {
                Specifications = await productSpecRepo.Object.GetAllEntitiesAsync(
                    new ProductSpecificationQuerySpecification()),
                Manufacturer = brand,
                ProductType = category
            }
        });

        var searchFilteringModel = new PersonalComputerFilteringModel();
        
        brand.Products = await productRepo.Object.GetAllEntitiesAsync(
            new PersonalComputerQuerySpecification(searchFilteringModel));
        productBrands.Object.UpdateExistingEntity(brand);

        category.Products = await productRepo.Object.GetAllEntitiesAsync(
            new PersonalComputerQuerySpecification(searchFilteringModel));
        productCategories.Object.UpdateExistingEntity(category);
        
        var result = await resolver.ResolveAsync(
            productRepo.Object, productSpecRepo.Object, productBrands.Object,
            productCategories.Object, searchFilteringModel);
        
        Assert.NotNull(result);
    }
}