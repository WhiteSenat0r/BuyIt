using Core.Validators;
using Core.Validators.Interfaces;

namespace Tests.Core.UnitTests.ProductRelatedTests.ValidatorsRelatedTests;

public class ProductSpecificationValidatorTests
{
    private IValidator _validator = null!;

    [Fact]
    public void Constructor_Should_ThrowArgumentExceptionIfProductTypeIsInvalid()
    {
        try
        {
            _validator = new ProductSpecificationValidator
                ("An invalid name", GetSpecifications());
        }
        catch (Exception e)
        {
            Assert.True(e is ArgumentException);
        }
    }
    
    [Fact]
    public void ValidateMethod_Should_NotThrowAnyExceptionsIfSpecificationIsValid()
    {
        _validator = GetFullyValidatorInstance();

        try
        {
            _validator.Validate();
        }
        catch (Exception e)
        {
            Assert.Fail("Exception was thrown");
        }
    }
    
    [Fact]
    public void ValidateMethod_Should_ThrowArgumentNullExceptionsIfAnySpecificationsKeyIsInvalid()
    {
        _validator = new ProductSpecificationValidator("Laptop", 
            new Dictionary<string, IDictionary<string, string>>
            {
                {
                    "", new Dictionary<string, string>
                    {
                        {
                            "Test", "test"
                        }
                    }
                }
            });

        Assert.Throws<ArgumentNullException>(_validator.Validate);
    }

    [Fact]
    public void ValidateMethod_Should_ThrowArgumentNullExceptionsIfAnySpecificationsValueIsInvalid()
    {
        _validator = new ProductSpecificationValidator("Laptop", 
            new Dictionary<string, IDictionary<string, string>>
            {
                {
                    "Test", null
                }
            });

        Assert.Throws<ArgumentNullException>(_validator.Validate);
    }
    
    [Fact]
    public void ValidateMethod_Should_ThrowArgumentNullExceptionsIfAnySpecsAttributeKeyIsInvalid()
    {
        _validator = new ProductSpecificationValidator("Laptop", 
            new Dictionary<string, IDictionary<string, string>>
            {
                {
                    "Test", new Dictionary<string, string>
                    {
                        {
                            " ", "test"
                        }
                    }
                }
            });

        Assert.Throws<ArgumentNullException>(_validator.Validate);
    }
    
    [Fact]
    public void ValidateMethod_Should_ThrowArgumentNullExceptionsIfAnySpecsAttributeValueIsInvalid()
    {
        _validator = new ProductSpecificationValidator("Laptop", 
            new Dictionary<string, IDictionary<string, string>>
            {
                {
                    "Test", new Dictionary<string, string>
                    {
                        {
                            "Test", " "
                        }
                    }
                }
            });

        Assert.Throws<ArgumentNullException>(_validator.Validate);
    }
    
    [Fact]
    public void ValidateMethod_Should_ThrowArgumentExceptionsIfAnySpecsKeyDoesNotMatchTemplate()
    {
        _validator = new ProductSpecificationValidator("Laptop", 
            new Dictionary<string, IDictionary<string, string>>
            {
                {
                    "General ", new Dictionary<string, string>
                    {
                        {
                            "Test", "Test"
                        }
                    }
                }
            });

        Assert.Throws<ArgumentException>(_validator.Validate);
    }
    
    [Fact]
    public void ValidateMethod_Should_ThrowArgumentExceptionsIfAnySpecsAttributeKeyDoesNotMatchTemplate()
    {
        _validator = new ProductSpecificationValidator("Laptop", 
            new Dictionary<string, IDictionary<string, string>>
            {
                {
                    "General", new Dictionary<string, string>
                    {
                        {
                            "Operating_null_system", "Test"
                        }
                    }
                }
            });

        Assert.Throws<ArgumentException>(_validator.Validate);
    }
    
    private static ProductSpecificationValidator GetFullyValidatorInstance() => 
        new("Laptop",
            GetSpecifications());

    private static Dictionary<string, IDictionary<string, string>> GetSpecifications()
    {
        return new Dictionary<string, IDictionary<string, string>>
        {
            {
                "Processor", new Dictionary<string, string>
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
                "General", new Dictionary<string, string>
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
                "Graphics card", new Dictionary<string, string>
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
                "Storage", new Dictionary<string, string>
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
                "Random access memory", new Dictionary<string, string>
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
                "Measurements", new Dictionary<string, string>
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
                "Interfaces and connection", new Dictionary<string, string>
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
                "Display", new Dictionary<string, string>
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
                "Battery", new Dictionary<string, string>
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
                "Additional", new Dictionary<string, string>
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
            }
        };
    }
}