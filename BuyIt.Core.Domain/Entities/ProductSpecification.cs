using Domain.Contracts.ProductRelated;

namespace Domain.Entities;

public sealed class ProductSpecification : IProductSpecification
{
    public ProductSpecification() { }

    public ProductSpecification
        (Guid categoryId, Guid attributeId, Guid valueId)
    {
        SpecificationCategoryId = categoryId;
        SpecificationAttributeId = attributeId;
        SpecificationValueId = valueId;
    }

    public Guid Id { get; } = Guid.NewGuid();

    public Guid SpecificationCategoryId { get; set; }
    
    public ProductSpecificationCategory SpecificationCategory { get; set; } = null!;
    
    public Guid SpecificationAttributeId { get; set; }

    public ProductSpecificationAttribute SpecificationAttribute { get; set; } = null!;
    
    public Guid SpecificationValueId { get; set; }

    public ProductSpecificationValue SpecificationValue { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}