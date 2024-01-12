using Domain.Contracts.Common;

namespace Domain.Contracts.ProductListRelated;

public interface IProductList<TProductItem> : IEntity<Guid>
    where TProductItem : IProductListItem
{
    IEnumerable<TProductItem> Items { get; set; }
}