using Core.Entities.Product.ProductSpecificationRelated.Common.Classes;

namespace Core.Entities.Product.ProductSpecificationRelated;

public sealed class ProductSpecificationAttribute : BasicSpecificationElement
{
    public ProductSpecificationAttribute() { }

    public ProductSpecificationAttribute(string attributeValue) : base(value:attributeValue) { }
}