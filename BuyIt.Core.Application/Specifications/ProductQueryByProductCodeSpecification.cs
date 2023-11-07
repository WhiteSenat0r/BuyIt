namespace Application.Specifications;

public sealed class ProductQueryByProductCodeSpecification : BasicProductQuerySpecification
{
    public ProductQueryByProductCodeSpecification(string productCode) 
        : base(criteria => criteria.ProductCode.ToLower().Equals(productCode.ToLower())) =>
        AddOrderByAscending(p => p.Name);
}