using Domain.Contracts.ProductListRelated;

namespace Domain.Entities.ProductListRelated;

public class ProductList<TProductItem> : IProductList<TProductItem> 
    where TProductItem : IProductListItem, new()
{
    public ProductList() { }

    public ProductList(Guid listId) => Id = listId;
    
    public Guid Id { get; set; }

    public IEnumerable<TProductItem> Items { get; set; } = new List<TProductItem>();
}