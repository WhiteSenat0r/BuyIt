using Domain.Common;

namespace Domain.Entities.ProductRelated;

public sealed class ProductSpecificationAttribute : BasicSpecificationElement
{
    public ProductSpecificationAttribute() { }

    public ProductSpecificationAttribute(string attributeValue) : base(value:attributeValue) { }
}