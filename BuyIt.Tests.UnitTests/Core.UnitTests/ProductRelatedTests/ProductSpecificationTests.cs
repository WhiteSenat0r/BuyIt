using Domain.Entities.ProductRelated;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.ProductRelatedTests;

public class ProductSpecificationTests
{
    [Fact]
    public void NewInstance_ShouldGenerateUniqueId()
    {
        var specification = new ProductSpecification();

        var id = specification.Id;

        Assert.NotEqual(Guid.Empty, id);
    }
    
    [Fact]
    public void NewSpecificationElementInstance_ShouldGenerateUniqueId()
    {
        var specificationValue = new ProductSpecificationValue();

        Assert.NotEqual(Guid.Empty, specificationValue.Id);
    }
    
    [Fact]
    public void NewSpecificationElementInstance_ShouldBeAbleToSetNewId()
    {
        var specificationValue = new ProductSpecificationValue("Value");

        Assert.NotNull(specificationValue);
    }
    
    [Fact]
    public void NewSpecificationElementInstance_ShouldBeAbleToSetAndGetSpecs()
    {
        var specificationValue = new ProductSpecificationAttribute("Value")
        {
            Specifications = new List<ProductSpecification>()
        };

        Assert.Empty(specificationValue.Specifications);
    }
    
    [Fact]
    public void NewSpecificationElementInstance_ShouldBeAbleToGetValue()
    {
        var specificationValue = new ProductSpecificationCategory("Value");

        Assert.NotEmpty(specificationValue.Value);
    }
    
    [Fact]
    public void ParameterizedConstructor_ShouldCreateNewInstance()
    {
        var specificationValue = new ProductSpecificationValue("Value");

        Assert.NotNull(specificationValue);
    }

    [Fact]
    public void SpecificationCategory_ShouldBeSettableAndGettable()
    {
        var specification = new ProductSpecification();
        var category = new ProductSpecificationCategory();
        
        specification.SpecificationCategory = category;
        var retrievedCategory = specification.SpecificationCategory;

        Assert.Equal(category, retrievedCategory);
    }

    [Fact]
    public void SpecificationAttribute_ShouldBeSettableAndGettable()
    {
        var specification = new ProductSpecification();
        var attribute = new ProductSpecificationAttribute();
        
        specification.SpecificationAttribute = attribute;
        var retrievedAttribute = specification.SpecificationAttribute;

        Assert.Equal(attribute, retrievedAttribute);
    }

    [Fact]
    public void SpecificationValue_ShouldBeSettableAndGettable()
    {
        var specification = new ProductSpecification();
        var value = new ProductSpecificationValue();

        specification.SpecificationValue = value;
        var retrievedValue = specification.SpecificationValue;

        Assert.Equal(value, retrievedValue);
    }
    
    [Fact]
    public void SpecificationElementIdProperties_ShouldBeSettableAndGettable()
    {
        var attribute = new ProductSpecificationAttribute();
        var category = new ProductSpecificationCategory();
        var value = new ProductSpecificationValue();
        
        var specification = new ProductSpecification(category.Id, attribute.Id, value.Id);

        specification.SpecificationAttributeId = Guid.NewGuid();
        specification.SpecificationCategoryId = Guid.NewGuid();
        specification.SpecificationValueId = Guid.NewGuid();

        Assert.NotEqual(attribute.Id, specification.SpecificationAttributeId);
        Assert.NotEqual(category.Id, specification.SpecificationCategoryId);
        Assert.NotEqual(value.Id, specification.SpecificationValueId);
    }
    
    [Fact]
    public void ProductsProperty_ShouldBeSettableAndGettable()
    {
        var products = new Product[] { new(), new() };
        
        var specification = new ProductSpecification();

        specification.Products = products;

        Assert.NotEmpty(specification.Products);
    }
}