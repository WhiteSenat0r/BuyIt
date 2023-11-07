using Domain.Common;

namespace Domain.Entities;

public sealed class ProductSpecificationAttribute : BasicSpecificationElement
{
    public ProductSpecificationAttribute() { }

    public ProductSpecificationAttribute(string attributeValue) : base(value:attributeValue) { }
}