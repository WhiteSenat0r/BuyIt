using System.ComponentModel.DataAnnotations;
using Core.Entities.Product.Common.Interfaces;

namespace Core.Entities.Product;

public class ProductType : IProductType
{
    private string _name = null!;

    public ProductType() { } // Required by EF Core for object's initialization from database

    public ProductType(string name) => Name = name; // Typically used in non-database initialization

    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(32)]
    public string Name
    {
        get => _name;
        set => AssignStringValue
            (value, ref _name);
    }

    private static void AssignStringValue(string text, ref string assignedVariable)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException
            ("String is null, empty or consists only of white spaces!",
                new InvalidDataException());
        assignedVariable = text;
    }
}