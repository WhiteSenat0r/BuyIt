using Domain.Common;

namespace Domain.Entities;

public sealed class ProductSpecificationCategory : BasicSpecificationElement
{
    public ProductSpecificationCategory() { }

    public ProductSpecificationCategory(string categoryValue) : base(value:categoryValue) { }
}