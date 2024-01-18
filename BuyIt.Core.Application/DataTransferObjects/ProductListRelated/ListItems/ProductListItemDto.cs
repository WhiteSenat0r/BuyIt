using System.ComponentModel.DataAnnotations;
using Application.Validation.Attributes.Links;
using Domain.Contracts.ProductListRelated;

namespace Application.DataTransferObjects.ProductListRelated.ListItems
{
    public abstract class ProductListItemDto : IProductListItem
    {
        [Required]
        public string Name { get; set; }
    
        [Required]
        public string Category { get; set; }
    
        [Required]
        public string ProductCode { get; set; }

        [Required]
        [ImageLinkValidity]
        public string ImageUrl { get; set; }
    }
}