using Application.DataTransferObjects.ProductRelated;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.DataTransferObjects;

public class GeneralizedProductDtoTests
{
    [Fact]
    public void GeneralizedProductDto_Properties_InitializedCorrectly()
    {
        var productDto = new GeneralizedProductDto();
        
        Assert.Null(productDto.Name);
        Assert.Equal(0, productDto.Price);
        Assert.Null(productDto.InStock);
        Assert.Null(productDto.Rating);
        Assert.Null(productDto.ProductCode);
        Assert.Null(productDto.Images);
        Assert.Null(productDto.Category);
    }

    [Fact]
    public void GeneralizedProductDto_Properties_Set_Get()
    {
        var productDto = new GeneralizedProductDto();

        productDto.Name = "Test Product";
        productDto.Price = 29.99m;
        productDto.InStock = "Yes";
        productDto.Rating = 4.5;
        productDto.ProductCode = "TP123";
        productDto.Images = new List<string> { "image1.jpg", "image2.jpg" };
        productDto.Category = "Test";

        Assert.Equal("Test Product", productDto.Name);
        Assert.Equal(29.99m, productDto.Price);
        Assert.Equal("Test", productDto.Category);
        Assert.Equal("Yes", productDto.InStock);
        Assert.Equal(4.5, productDto.Rating);
        Assert.Equal("TP123", productDto.ProductCode);
        Assert.Collection(productDto.Images, 
            img1 => Assert.Equal("image1.jpg", img1),
            img2 => Assert.Equal("image2.jpg", img2));
    }
}