using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects.ProductListRelated.ListItems
{
    public sealed class WishedItemDto : PricedProductListItemDto
    {
        [Required]
        [RegularExpression("^(true|false)$",
            ErrorMessage = "\"In stock\" must be either 'true' or 'false'!")]
        public string InStock { get; set; }
    }
}