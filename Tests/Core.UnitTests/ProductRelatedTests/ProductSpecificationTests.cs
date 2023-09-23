using Core.Entities.Product.ProductSpecificationRelated;

namespace Tests.Core.UnitTests.ProductRelatedTests;

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
}