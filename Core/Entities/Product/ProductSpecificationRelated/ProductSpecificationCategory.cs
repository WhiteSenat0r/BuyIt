using Core.Entities.Product.ProductSpecificationRelated.Common.Classes;

namespace Core.Entities.Product.ProductSpecificationRelated;

public sealed class ProductSpecificationCategory : BasicSpecificationElement
{
    public ProductSpecificationCategory() { }

    public ProductSpecificationCategory(string categoryValue) : base(value:categoryValue) { }
}