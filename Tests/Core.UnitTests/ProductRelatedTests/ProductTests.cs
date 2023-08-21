using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Core.Entities.Product;
using Core.Entities.Product.Common.Interfaces;

namespace Tests.Core.UnitTests.ProductRelatedTests;

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
        Assert.True(_product.ManufacturerId != Guid.Empty);
        Assert.True(_product.ProductTypeId != Guid.Empty);
        Assert.True(_product.RatingId != Guid.Empty);
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
    public void ProductCodeProperty_Should_EqualFirstEightGuidsChars()
    {
        _product = new Product();
        
        Assert.Equal(_product.ProductCode, _product.Id.ToString()[..8].ToUpper());
    }
    
    [Fact]
    public void MainImagesUrlsProperty_Should_ThrowArgumentNullExceptionIfEmptyOrNull()
    {
        Assert.Throws<ArgumentNullException>(() => _product = new Product()
        {
            MainImagesUrls = new List<string>()
        });
        
        Assert.Throws<ArgumentNullException>(() => _product = new Product()
        {
            MainImagesUrls = null
        });
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
        "ASUS ZenBook Pro Space Black",
        "Some description",
        1499.99m,
        true,
        new ProductManufacturer
            ("Apple", "United States of America"),
        new ProductType("Laptop"),
        new ProductRating(5),
        new List<string>
        {
            "https://somewebpage.com/iphone14promax1.jpg",
            "https://somewebpage.com/iphone14promax2.jpg",
            "https://somewebpage.com/iphone14promax3.jpg",
            "https://somewebpage.com/iphone14promax4.jpg",
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
                    "Series", "Intel Iris"
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
                    "Weight", "1.5 Kg"
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
                    "Type", "Li-ion"
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