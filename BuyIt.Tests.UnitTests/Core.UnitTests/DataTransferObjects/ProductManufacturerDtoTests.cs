using Application.DataTransferObjects.Manufacturer;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.DataTransferObjects;

public class ProductManufacturerDtoTests
{
    [Fact]
    public void Brand_SetAndGet_Success()
    {
        var productManufacturerDto = new ProductManufacturerDto();
        var brand = "TestBrand";

        productManufacturerDto.Brand = brand;
        var result = productManufacturerDto.Brand;

        Assert.Equal(brand, result);
    }
}