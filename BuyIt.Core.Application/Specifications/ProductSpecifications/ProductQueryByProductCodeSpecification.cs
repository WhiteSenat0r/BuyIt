namespace Application.Specifications.ProductSpecifications;

public sealed class ProductQueryByProductCodeSpecification : BasicProductQuerySpecification
{
    public ProductQueryByProductCodeSpecification(string productCode) 
        : base(criteria => criteria.ProductCode.ToLower().Equals(productCode.ToLower()))
    {
        AddInclude("Manufacturer");
        AddInclude("ProductType");
        AddInclude("Rating");
        AddInclude("Specifications.SpecificationCategory");
        AddInclude("Specifications.SpecificationAttribute");
        AddInclude("Specifications.SpecificationValue");
    }
}