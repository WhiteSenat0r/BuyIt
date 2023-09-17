using Core.Entities.Product.ProductSpecification.Common.Classes;

namespace Core.Entities.Product.ProductSpecification;

public sealed class ProductSpecificationAttribute : BasicSpecificationElement
{
    public ProductSpecificationAttribute() { }

    public ProductSpecificationAttribute(string attributeValue) : base(value:attributeValue) { }
}