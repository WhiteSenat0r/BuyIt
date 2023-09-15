using System.ComponentModel.DataAnnotations;
using Core.Entities.Product;
using Core.Entities.Product.ProductSpecification;

namespace Tests.Core.UnitTests.ProductRelatedTests;

public class ProductSpecificationTests
{
    [Fact]
    public void New_ProductSpecification_Should_Have_NewGuid_Id()
    {
        var productSpec = new ProductSpecification();

        Assert.NotEqual(Guid.Empty, productSpec.Id);
    }

    [Fact]
    public void ProductSpecification_Category_Should_Have_MaxLength48Attribute()
    {
        var productSpec = new ProductSpecification();
        
        var maxLengthAttribute = typeof(ProductSpecification)
            .GetProperty("Category")
            ?.GetCustomAttributes(typeof(MaxLengthAttribute), false)
            as MaxLengthAttribute[];

        Assert.NotNull(maxLengthAttribute);
        Assert.Single(maxLengthAttribute);
        Assert.Equal(48, maxLengthAttribute[0].Length);
    }

    [Fact]
    public void ProductSpecification_Attribute_Should_Have_MaxLength48Attribute()
    {
        var productSpec = new ProductSpecification();

        var maxLengthAttribute = typeof(ProductSpecification)
            .GetProperty("Attribute")
            ?.GetCustomAttributes(typeof(MaxLengthAttribute), false)
            as MaxLengthAttribute[];
        
        Assert.NotNull(maxLengthAttribute);
        Assert.Single(maxLengthAttribute);
        Assert.Equal(48, maxLengthAttribute[0].Length);
    }

    [Fact]
    public void ProductSpecification_Value_Should_Have_MaxLength192Attribute()
    {
        var productSpec = new ProductSpecification();

        var maxLengthAttribute = typeof(ProductSpecification)
            .GetProperty("Value")
            ?.GetCustomAttributes(typeof(MaxLengthAttribute), false)
            as MaxLengthAttribute[];

        Assert.NotNull(maxLengthAttribute);
        Assert.Single(maxLengthAttribute);
        Assert.Equal(192, maxLengthAttribute[0].Length);
    }

    [Fact]
    public void ProductSpecification_Should_Set_Properties_Using_Constructor()
    {
        var category = "Test Category";
        var attribute = "Test Attribute";
        var value = "Test Value";
        var productId = Guid.NewGuid();

        var productSpec = new ProductSpecification(category, attribute, value, productId);

        Assert.Equal(category, productSpec.Category);
        Assert.Equal(attribute, productSpec.Attribute);
        Assert.Equal(value, productSpec.Value);
        Assert.Equal(productId, productSpec.ProductId);
    }

    [Fact]
    public void ProductSpecification_Product_Should_Be_NullByDefault()
    {
        var productSpec = new ProductSpecification();

        Assert.Null(productSpec.Product);
    }
}