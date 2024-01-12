using Domain.Contracts.ProductListRelated;

namespace Domain.Common;

public abstract class ProductListItem : IProductListItem
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Category { get; set; }
    
    public string ProductCode { get; set; }

    public string ImageUrl { get; set; }
}