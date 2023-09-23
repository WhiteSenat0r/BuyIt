using Core.Entities.Product.ProductSpecificationRelated.Common.Classes;

namespace Core.Entities.Product.ProductSpecificationRelated;

public sealed class ProductSpecificationValue : BasicSpecificationElement
{
    public ProductSpecificationValue() { }

    public ProductSpecificationValue(string value) : base(value) { }
}