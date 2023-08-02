using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Core.Entities.Product.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Core.Entities.Product;

public class Product : IProduct
{
    private string _name = null!;
    private string _description = null!;
    private decimal _price;
    private IDictionary<string, IEnumerable<string>> _mainImagesUrls = null!;
    private IEnumerable<string>? _descriptionImagesUrls;

    public Product() { }

    public Product
    (
        string name,
        string description,
        decimal price,
        bool inStock,
        ProductManufacturer manufacturer,
        ProductType productType,
        ProductRating rating,
        IDictionary<string, IEnumerable<string>> mainImagesUrls,
        IEnumerable<string>? descriptionImagesUrls,
        IDictionary<string, IDictionary<string, string>> specifications)
    {
        Name = name;
        Description = description;
        Price = price;
        InStock = inStock;
        Manufacturer = manufacturer;
        ProductType = productType;
        Rating = rating;
        MainImagesUrls = mainImagesUrls;
        DescriptionImagesUrls = descriptionImagesUrls;
        Specifications = specifications;
    }
    
    public Guid Id { get; set; } = Guid.NewGuid(); // Id of the product
    
    [MaxLength(96)]
    public string Name // Name of the product
    {
        get => _name;
        set => AssignStringValue
            (value, ref _name);
    }
    
    [MaxLength(1024)]
    public string Description // Description of the product
    {
        get => _description;
        set => AssignStringValue
            (value, ref _description);
    }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Price // Price of the product (for example: $USD 199.99)
    {
        get => _price;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Price must be greater than 0!");
            _price = value;
        }
    }

    public bool InStock { get; set; } // Indicator of the product's availability

    public ProductManufacturer Manufacturer { get; set; } = null!; // Manufacturer reference
    
    public Guid ManufacturerId { get; set; }

    public ProductRating Rating { get; set; } = null!; // Rating reference
    
    public Guid RatingId { get; set; }

    public ProductType ProductType { get; set; } = null!; // Product type reference
    
    public Guid ProductTypeId { get; set; }

    public IDictionary<string, IEnumerable<string>> MainImagesUrls // Dictionary of the product's images:
                                                                   // key: color, value: urls with images of the product.
                                                                   // Every pair contains its color variation of the item
                                                                   // and images related to it.
    {
        get => _mainImagesUrls;
        set
        {
            foreach (var urls in value)
                CheckUrlsValidity(urls.Value);
            _mainImagesUrls = value;
        }
    }
    
    public IEnumerable<string>? DescriptionImagesUrls // Urls that can be used in the product's description
    {
        get => _descriptionImagesUrls;
        set
        {
            CheckUrlsValidity(value);
            _descriptionImagesUrls = value;
        }
    }

    public IDictionary<string, IDictionary<string, string>> // Specifications of the product (can easily vary due to the
                                                            // usage of IDictionary)
        Specifications { get; set; } = null!;
    
    private static void AssignStringValue
        (string text, ref string assignedVariable)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException
            ("String is null, empty or consists only of white spaces!",
                new InvalidDataException());
        assignedVariable = text;
    }

    private static void CheckUrlsValidity(IEnumerable<string> urls)
    {
        if (urls.IsNullOrEmpty())
            return;
        var pattern = new Regex(@"\bhttps?:\/\/\S+?\.(?:png|jpe?g)\b");
        if (urls.Any(url => !pattern.IsMatch(url)))
            throw new InvalidDataException("URL of the image has incorrect format!");
    }
}