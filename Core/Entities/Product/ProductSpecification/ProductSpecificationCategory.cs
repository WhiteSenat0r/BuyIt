using Core.Entities.Product.ProductSpecification.Common.Classes;

namespace Core.Entities.Product.ProductSpecification;

public sealed class ProductSpecificationCategory : BasicSpecificationElement
{
    public ProductSpecificationCategory() { }

    public ProductSpecificationCategory(string categoryValue) : base(value:categoryValue) { }
}