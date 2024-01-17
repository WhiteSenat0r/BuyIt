namespace Domain.Contracts.ProductListRelated;

public interface IProductListItem
{
    string Name { get; set; }
    
    string Category { get; set; }
    
    string ProductCode { get; set; }
    
    string ImageUrl { get; set; }
}