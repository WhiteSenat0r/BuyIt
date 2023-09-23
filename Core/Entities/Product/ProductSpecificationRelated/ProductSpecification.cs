using Core.Entities.Product.Common.Interfaces;

namespace Core.Entities.Product.ProductSpecificationRelated;

public sealed class ProductSpecification : IProductSpecification
{
    public ProductSpecification() { }

    public ProductSpecification
        (Guid categoryId, Guid attributeId, Guid valueId, Guid productId)
    {
        SpecificationCategoryId = categoryId;
        SpecificationAttributeId = attributeId;
        SpecificationValueId = valueId;
        ProductId = productId;
    }

    public Guid Id { get; } = Guid.NewGuid();

    public Guid SpecificationCategoryId { get; set; }
    
    public ProductSpecificationCategory SpecificationCategory { get; set; } = null!;
    
    public Guid SpecificationAttributeId { get; set; }

    public ProductSpecificationAttribute SpecificationAttribute { get; set; } = null!;
    
    public Guid SpecificationValueId { get; set; }

    public ProductSpecificationValue SpecificationValue { get; set; } = null!;

    public Guid ProductId { get; set; }
    
    public Product Product { get; set; } = null!;
}