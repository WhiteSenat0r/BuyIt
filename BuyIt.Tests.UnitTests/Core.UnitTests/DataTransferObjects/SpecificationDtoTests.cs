using Application.DataTransferObjects.ProductRelated.Specification;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.DataTransferObjects;

public class SpecificationDtoTests
{
    [Fact]
    public void Properties_SetAndGet_Success()
    {
        var specificationDto = new SpecificationDto
        {
            Category = "TestCategory",
            Attribute = "TestAttribute",
            Value = "TestValue"
        };

        var category = specificationDto.Category;
        var attribute = specificationDto.Attribute;
        var value = specificationDto.Value;

        Assert.Equal("TestCategory", category);
        Assert.Equal("TestAttribute", attribute);
        Assert.Equal("TestValue", value);
    }

    [Fact]
    public void ToString_ReturnsExpectedString_Success()
    {
        var specificationDto = new SpecificationDto
        {
            Category = "TestCategory",
            Attribute = "TestAttribute",
            Value = "TestValue"
        };

        var result = specificationDto.ToString();

        Assert.Equal("Category:TestCategory|Attribute:TestAttribute|Value:TestValue", result);
    }
}