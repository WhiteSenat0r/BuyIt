using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Domain.Contracts.ProductRelated;
using Domain.Entities;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.ProductRelatedTests;

public class ProductManufacturerTests
{
    private IProductManufacturer _productManufacturer = null!;
    
    [Fact]
    public void EmptyConstructor_Should_CreateNewObject()
    {
        _productManufacturer = new ProductManufacturer();
        
        Assert.True(_productManufacturer.GetType() == typeof(ProductManufacturer));
    }
    
    [Fact]
    public void FullDataConstructor_Should_CreateNewFullyInitializedObject()
    {
        _productManufacturer = GetFullyInitializedProductManufacturer();

        Assert.True(_productManufacturer.GetType() == typeof(ProductManufacturer));
        Assert.True(_productManufacturer.Name is not null);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void StringTypeProperties_Should_ThrowArgumentNullExceptionIfNullOrEmpty(string value)
    {
        _productManufacturer = new ProductManufacturer();
    
        var stringProperties = GetStringPropertiesFromClass();
    
        foreach (var property in stringProperties)
            AssertThrownException(typeof(ArgumentNullException), property, _productManufacturer, value);
    }

    [Fact]
    public void StringTypeProperties_Should_ThrowArgumentExceptionIfStringLengthIsGreaterThanMaxLength()
    {
        _productManufacturer = new ProductManufacturer();
    
        var stringProperties = GetStringPropertiesFromClass();
    
        foreach (var stringProperty in stringProperties)
        {
            var maxLengthAttribute = (MaxLengthAttribute)Attribute.
                GetCustomAttribute(stringProperty, typeof(MaxLengthAttribute))!;
            
            var stringBuilder = new StringBuilder();
    
            for (var i = 0; i < maxLengthAttribute.Length + 1; i++)
                stringBuilder.Append('c');
            
            AssertThrownException
                (typeof(ArgumentException), stringProperty, _productManufacturer, stringBuilder.ToString());
        }
    }

    [Fact]
    public void IdProperty_Should_NotBeEmpty()
    {
        _productManufacturer = new ProductManufacturer();

        Assert.NotEqual(Guid.Empty, _productManufacturer.Id);
    }

    private static ProductManufacturer GetFullyInitializedProductManufacturer() =>
        new ("Manufacturer");
    
    private List<PropertyInfo> GetStringPropertiesFromClass() => 
        _productManufacturer.GetType().GetProperties().Where
            (p => p.PropertyType == typeof(string)).ToList();
    
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