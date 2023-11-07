using System.Reflection;
using Domain.Contracts.ProductRelated;
using Domain.Entities;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.ProductRelatedTests;

public class ProductTests
{
    private IProduct _product = null!;
    
    [Fact]
    public void EmptyConstructor_Should_CreateNewObject()
    {
        _product = new Product();
        
        Assert.True(_product.GetType() == typeof(Product));
    }
    
    [Fact]
    public void FullDataConstructor_Should_CreateNewFullyInitializedObject()
    {
        _product = GetFullyInitializedProduct();

        Assert.True(_product.GetType() == typeof(Product));
        Assert.True(_product.Name is not null);
        Assert.True(_product.Description is not null);
        Assert.True(_product.MainImagesNames is not null);
        Assert.True(_product.ManufacturerId != Guid.Empty);
        Assert.True(_product.ProductTypeId != Guid.Empty);
        Assert.True(_product.RatingId != Guid.Empty);
    }

    [Fact]
    public void IdProperty_Should_ContainGuidAfterInitialization()
    {
        _product = new Product();
    
        Assert.True(_product.Id != Guid.Empty);
    
        _product = GetFullyInitializedProduct();
    
        Assert.True(_product.Id != Guid.Empty);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void StringTypeProperties_Should_ThrowArgumentNullExceptionIfNullOrEmpty(string value)
    {
        _product = new Product();
    
        var stringProperties = GetStringPropertiesFromClass();
    
        foreach (var property in stringProperties)
            AssertThrownException(typeof(ArgumentNullException), property, _product, value);
    }

    [Fact]
    public void PriceProperty_Should_ThrowArgumentExceptionIfValueEqualsOrLessThanZero()
    {
        _product = new Product();
    
        Assert.Throws<ArgumentException>(() => _product.Price = 0);
        Assert.Throws<ArgumentException>(() => _product.Price = -0.1m);
    }
    
    [Fact]
    public void PriceProperty_Should_BeAbleToReturnPriceValue()
    {
        _product = GetFullyInitializedProduct();

        var priceValue = _product.Price;
        
        Assert.Equal(priceValue, _product.Price);
    }
    
    [Fact]
    public void InStockProperty_Should_DefaultToFalse()
    {
        _product = new Product();
        
        Assert.False(_product.InStock);
    }
    
    [Fact]
    public void ManufacturerProperty_Should_DefaultNotBeInitialized()
    {
        _product = new Product();
        
        Assert.Null(_product.Manufacturer);
    }
    
    [Fact]
    public void ManufacturerIdProperty_Should_DefaultBeEmptyGuid()
    {
        _product = new Product();
        
        Assert.Equal(Guid.Empty, _product.ManufacturerId);
    }
    
    [Fact]
    public void RatingProperty_Should_DefaultNotBeInitialized()
    {
        _product = new Product();
        
        Assert.Null(_product.Rating);
    }
    
    [Fact]
    public void RatingIdProperty_Should_DefaultBeEmptyGuid()
    {
        _product = new Product();
        
        Assert.Equal(Guid.Empty, _product.RatingId);
    }
    
    [Fact]
    public void ProductTypeProperty_Should_DefaultNotBeInitialized()
    {
        _product = new Product();
        
        Assert.Null(_product.ProductType);
    }
    
    [Fact]
    public void ProductTypeIdProperty_Should_DefaultBeEmptyGuid()
    {
        _product = new Product();
        
        Assert.Equal(Guid.Empty, _product.ProductTypeId);
    }
    
    [Fact]
    public void ProductCodeProperty_Should_EqualFirstEightGuidsChars()
    {
        _product = new Product();
        
        Assert.Equal(_product.ProductCode, _product.Id.ToString()[..8].ToUpper());
    }
    
    [Fact]
    public void MainImagesUrlsProperty_Should_ThrowArgumentNullExceptionIfEmptyOrNull()
    {
        Assert.Throws<ArgumentNullException>(() => _product = new Product
        {
            MainImagesNames = new List<string>()
        });
        
        Assert.Throws<ArgumentNullException>(() => _product = new Product
        {
            MainImagesNames = null
        });
    }

    private List<PropertyInfo> GetStringPropertiesFromClass() => 
        _product.GetType().GetProperties().Where
            (p => p.PropertyType == typeof(string)).ToList();

    private static Product GetFullyInitializedProduct() => new(
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
        new ProductManufacturer("Apple"),
        new ProductType("Personal computer"),
        new ProductRating(null),
        new List<string>
        {
            "1.jpg",
            "2.jpg",
            "3.jpg",
            "4.jpg"
        });
    
    private static void AssertThrownException
        (Type exceptionType, PropertyInfo stringProperty, object obj, string text)
    {
        try
        {
            stringProperty.SetValue(obj, text);
        }
        catch (Exception e)
        {
            Assert.True(e.InnerException!.GetType() == exceptionType);
        }
    }
}