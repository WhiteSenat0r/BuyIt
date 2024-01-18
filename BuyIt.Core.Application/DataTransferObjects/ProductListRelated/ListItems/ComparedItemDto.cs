using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects.ProductListRelated.ListItems
{
    public sealed class ComparedItemDto : ProductListItemDto
    {
        [Required]
        public IDictionary<string, IDictionary<string,string>> Specifications { get; set; }
    }
}