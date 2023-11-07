using Domain.Common;

namespace Domain.Entities;

public sealed class ProductSpecificationValue : BasicSpecificationElement
{
    public ProductSpecificationValue() { }

    public ProductSpecificationValue(string value) : base(value) { }
}