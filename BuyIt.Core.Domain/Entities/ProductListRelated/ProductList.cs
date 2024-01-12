using Domain.Contracts.ProductListRelated;

namespace Domain.Entities.ProductListRelated;

public class ProductList<TProductItem> : IProductList<IProductListItem>
    where TProductItem : IProductListItem
{
    public ProductList() { }

    public ProductList(Guid listId) => Id = listId;
    
    public Guid Id { get; set; }

    public IEnumerable<IProductListItem> Items { get; set; }
}