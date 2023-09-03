using System.ComponentModel.DataAnnotations;
using Core.Entities.Product.Common.Interfaces;

namespace Core.Entities.Product;

public class ProductSpecification : IProductSpecification
{
    public ProductSpecification() { }

    public ProductSpecification
        (string category, string attribute, string value, Guid productId)
    {
        Category = category;
        Attribute = attribute;
        Value = value;
        ProductId = productId;
    }

    public Guid Id { get; } = Guid.NewGuid();
    
    [MaxLength(48)]
    public string Category { get; set; }

    [MaxLength(48)]
    public string Attribute { get; set; } 

    [MaxLength(192)]
    public string Value { get; set; }

    public Guid ProductId { get; set; }
    
    public Product Product { get; set; } = null!;
}