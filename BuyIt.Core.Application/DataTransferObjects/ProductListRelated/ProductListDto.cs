using System.ComponentModel.DataAnnotations;
using Domain.Contracts.ProductListRelated;

namespace Application.DataTransferObjects.ProductListRelated;

public class ProductListDto<TProductItem> : IProductList<TProductItem> 
    where TProductItem : IProductListItem, new()
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public IEnumerable<TProductItem> Items { get; set; }
}