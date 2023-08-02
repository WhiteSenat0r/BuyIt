using System.ComponentModel.DataAnnotations;
using Core.Entities.Product.Common.Interfaces;

namespace Core.Entities.Product;

public class ProductManufacturer : IProductManufacturer
{
    private string _name = null!;
    private string _registrationCountry = null!;

    public ProductManufacturer() { }
    
    public ProductManufacturer
        (Product product, string name, string registrationCountry)
    {
        Product = product;
        Name = name;
        RegistrationCountry = registrationCountry;
    }
    
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(32)]
    public string Name
    {
        get => _name;
        set => AssignStringValue
            (value, ref _name);
    }
    
    [MaxLength(32)]
    public string RegistrationCountry
    {
        get => _registrationCountry;
        set => AssignStringValue
            (value, ref _registrationCountry);
    }

    public Product Product { get; set; } = null!;

    private static void AssignStringValue(string text, ref string assignedVariable)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException
            ("String is null, empty or consists only of white spaces!",
                new InvalidDataException());
        assignedVariable = text;
    }
}