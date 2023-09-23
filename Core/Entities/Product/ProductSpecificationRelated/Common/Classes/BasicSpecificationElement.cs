using System.ComponentModel.DataAnnotations;
using Core.Entities.Product.ProductSpecificationRelated.Common.Interfaces;

namespace Core.Entities.Product.ProductSpecificationRelated.Common.Classes;

public abstract class BasicSpecificationElement : ISpecificationAspect
{
    private readonly string _value = null!;

    protected BasicSpecificationElement() { }
    
    protected BasicSpecificationElement(string value) => Value = value;
    
    public Guid Id { get; } = Guid.NewGuid();
    
    public ICollection<ProductSpecification> Specifications { get; set; }
    
    [MaxLength(256)]
    public string Value
    {
        get => _value;
        init
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(Value), "Passed value is null!");
            _value = value;
        }
    }
}