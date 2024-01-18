using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects.ProductListRelated.ListItems
{
    public abstract class PricedProductListItemDto : ProductListItemDto
    {
        [Required]
        [Range((double)0.01m, (double)decimal.MaxValue, ErrorMessage = "Price must be greater than 0!")]
        public decimal Price { get; set; }
    }
}