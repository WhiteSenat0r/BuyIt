using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Core.Entities.Product;
using Core.Entities.Product.Common.Interfaces;

namespace Tests.ProductRelatedTests;

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
        Assert.True(_product.MainImagesUrls is not null);
        Assert.True(_product.Manufacturer is not null);
        Assert.True(_product.ProductType is not null);
        Assert.True(_product.Rating is not null);
        Assert.True(_product.Specifications is not null);
    }

    [Fact]
    public void IdProperty_Should_ContainGuidAfterInitialization()
    {
        _product = new Product();
    
        Assert.True(_product.Id != Guid.Empty);
    
        _product = GetFullyInitializedProduct();
    
        Assert.True(_product.Id != Guid.Empty);
    }
    
    [Fact]
    public void IdProperty_Should_BeAbleToSetNewGuid()
    {
        _product = new Product();

        var initialProductId = _product.Id;

        _product.Id = Guid.NewGuid();
        
        Assert.NotEqual(initialProductId, _product.Id);
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
    public void StringTypeProperties_Should_ThrowArgumentExceptionIfStringLengthIsGreaterThanMaxLength()
    {
        _product = new Product();
    
        var stringProperties = GetStringPropertiesFromClass();
    
        foreach (var stringProperty in stringProperties)
        {
            var maxLengthAttribute = (MaxLengthAttribute)Attribute.
                GetCustomAttribute(stringProperty, typeof(MaxLengthAttribute))!;
            
            var stringBuilder = new StringBuilder();
    
            for (var i = 0; i < maxLengthAttribute.Length + 1; i++)
                stringBuilder.Append('c');
            
            AssertThrownException
                (typeof(ArgumentException), stringProperty, _product, stringBuilder.ToString());
        }
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
    public void DescriptionImagesUrlsProperty_Should_DefaultNotBeInitialized()
    {
        _product = new Product();
        
        Assert.Null(_product.DescriptionImagesUrls);
    }
    
    [Fact]
    public void DescriptionImagesUrlsProperty_Should_ThrowInvalidDataExceptionIfInvalidUrlsWerePassed()
    {
        _product = new Product();
        
        Assert.Throws<InvalidDataException>(() => 
            _product.DescriptionImagesUrls = new List<string>()
        {
            "https://validurl.com/image.jpg",
            "Invalid URL"
        });
    }
    
    [Fact]
    public void DescriptionImagesUrlsProperty_Should_BeEmptyIfEmptyListWasPassed()
    {
        _product = new Product();
        
        Assert.Empty(_product.DescriptionImagesUrls = new List<string>());
    }
    
    [Fact]
    public void SpecificationsProperty_Should_ThrowArgumentNullExceptionIfNullIsPassed()
    {
        Assert.Throws<ArgumentNullException>(() => _product = new Product()
        {
            Specifications = null
        });
    }
    
    [Theory]
    [MemberData(nameof
        (SpecificationsPropertyShouldThrowArgumentNullExceptionIfKeyOrValueIsNullOrEmptyTestData))]
    public void SpecificationsProperty_Should_ThrowArgumentNullExceptionIfKeyOrValueIsNullOrEmpty
        (string key, IDictionary<string,string> attributes)
    {
        Assert.Throws<ArgumentNullException>(() => _product = new Product()
        {
            Specifications = new Dictionary<string, IDictionary<string, string>>()
            {
                {
                    key, attributes
                }
            }
        });
    }
    
    [Theory]
    [MemberData(nameof
        (SpecificationsPropertyShouldThrowArgumentNullExceptionIfValuesKeyOrValueIsNullOrEmptyTestData))]
    public void SpecificationsProperty_Should_ThrowArgumentNullExceptionIfValuesKeyOrValueIsNullOrEmpty
        (string key, string value)
    {
        Assert.Throws<ArgumentNullException>(() => _product = new Product()
        {
            Specifications = new Dictionary<string, IDictionary<string, string>>()
            {
                {
                    "Key", new Dictionary<string, string>()
                    {
                        {
                            key, value
                        }
                    }
                }
            }
        });
    }

    public static IEnumerable<object[]> 
        SpecificationsPropertyShouldThrowArgumentNullExceptionIfKeyOrValueIsNullOrEmptyTestData =>
        new List<object[]>
        {
            new object[] { "Key", null! },
            new object[] { "Key", new Dictionary<string, string>() },
            new object[]
            {
                null!, new Dictionary<string, string>
                {
                    {
                        "Attribute1", "AttributeValue1"
                    }
                }
            },
            new object[]
            {
                "", new Dictionary<string, string>()
                {
                    {
                        "Attribute1", "AttributeValue1"
                    }
                }
            }
        };
    
    public static IEnumerable<object[]> 
        SpecificationsPropertyShouldThrowArgumentNullExceptionIfValuesKeyOrValueIsNullOrEmptyTestData =>
        new List<object[]>
        {
            new object[] { "Key", null! },
            new object[] { "Key", "" },
            new object[] { null!, "Value" },
            new object[] { "", "Value" },
        };

    
    private List<PropertyInfo> GetStringPropertiesFromClass() => 
        _product.GetType().GetProperties().Where
            (p => p.PropertyType == typeof(string)).ToList();

    private static Product GetFullyInitializedProduct() => new(
        "Apple iPhone 14 Pro Max",
        "Some description",
        1499.99m,
        true,
        new ProductManufacturer
            ("Apple", "United States of America"),
        new ProductType("Cellphone"),
        new ProductRating(5),
        new Dictionary<string, IEnumerable<string>>()
        {
            {
                "Space Black", new List<string>()
                {
                    "https://somewebpage.com/iphone14promax1.jpg",
                    "https://somewebpage.com/iphone14promax2.jpg",
                    "https://somewebpage.com/iphone14promax3.jpg",
                    "https://somewebpage.com/iphone14promax4.jpg",
                }
            }
        },
        new List<string>()
        {
            "https://somewebpage.com/iphone14promax1.jpg",
            "https://somewebpage.com/iphone14promax2.jpg",
            "https://somewebpage.com/iphone14promax3.jpg",
            "https://somewebpage.com/iphone14promax4.jpg",
        },
        new Dictionary<string, IDictionary<string, string>>()
        {
            {
                "FirstSpec", new Dictionary<string, string>()
                {
                    {
                        "FirstAttribute", "FirstAttributeValue"
                    }
                }
            }
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