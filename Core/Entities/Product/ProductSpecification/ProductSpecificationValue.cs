using Core.Entities.Product.ProductSpecification.Common.Classes;

namespace Core.Entities.Product.ProductSpecification;

public sealed class ProductSpecificationValue : BasicSpecificationElement
{
    public ProductSpecificationValue() { }

    public ProductSpecificationValue(string value) : base(value) { }
}