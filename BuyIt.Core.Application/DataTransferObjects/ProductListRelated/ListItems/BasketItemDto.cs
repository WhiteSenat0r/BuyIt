using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects.ProductListRelated.ListItems
{
    public sealed class BasketItemDto : PricedProductListItemDto
    {
        [Required]
        [Range(1, 999, ErrorMessage = "Quantity must be in range from 1 to 999!")]
        public int Quantity { get; set; }
    }
}