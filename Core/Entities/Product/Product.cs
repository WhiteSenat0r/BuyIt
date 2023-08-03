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
    private IDictionary<string, IDictionary<string, string>> _specifications;

    public Product() { } // Required by EF Core for object's initialization from database

    public Product // Typically used in non-database initialization
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
        ManufacturerId = Manufacturer.Id;
        ProductTypeId = ProductType.Id;
        RatingId = Rating.Id;
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

    public IDictionary<string, IEnumerable<string>> MainImagesUrls                                        
    {
        get => _mainImagesUrls;
        set
        {
            foreach (var urls in value)
                CheckUrlsValidity(urls.Value);
            _mainImagesUrls = value;
        }
    }
    
    public IEnumerable<string>? DescriptionImagesUrls
    {
        get => _descriptionImagesUrls;
        set
        {
            CheckUrlsValidity(value);
            if (!value!.Any())
                value = null;
            _descriptionImagesUrls = value;
        }
    }

    public IDictionary<string, IDictionary<string, string>> Specifications
    {
        get => _specifications;
        set
        {
            if (value is null) 
                ThrowArgumentNullException("Specifications can not be null!");
            CheckSpecificationsValues(value);
            _specifications = value;
        }
    }

    private static void CheckSpecificationsValues
        (IDictionary<string, IDictionary<string, string>> value)
    {
        foreach (var specificationPair in value!)
        {
            if (string.IsNullOrEmpty(specificationPair.Key) ||
                string.IsNullOrWhiteSpace(specificationPair.Key))
                ThrowArgumentNullException
                    ("One of specifications' keys is null, empty or consists only of whitespaces!");
            if (specificationPair.Value.IsNullOrEmpty())
                ThrowArgumentNullException("One of specifications' values is null!");
            foreach (var attributePair in specificationPair.Value!)
            {
                if (string.IsNullOrEmpty(attributePair.Key) ||
                    string.IsNullOrWhiteSpace(attributePair.Key))
                    ThrowArgumentNullException
                        ("One of attributes' keys is null, empty or consists only of whitespaces!");
                if (string.IsNullOrEmpty(attributePair.Value) ||
                    string.IsNullOrWhiteSpace(attributePair.Value))
                    ThrowArgumentNullException
                        ("One of attributes' values is null, empty or consists only of whitespaces!");
            }
        }
    }

    private static void AssignStringValue
        (string text, ref string assignedVariable)
    {
        CheckStringValidity(text);
        
        var propertyInfo = typeof(Product).GetProperty("Name");
        
        var maxLengthAttribute = (MaxLengthAttribute)Attribute.
            GetCustomAttribute(propertyInfo!, typeof(MaxLengthAttribute))!;

        if (maxLengthAttribute is not null &&
            maxLengthAttribute.Length < text.Length)
            throw new ArgumentException("String's length is greater that maximum allowed length!");
            
        assignedVariable = text;
    }

    private static void CheckStringValidity(string text)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)) 
            ThrowArgumentNullException
                ("String is null, empty or consists only of white spaces!");
    }

    private static void ThrowArgumentNullException(string message) =>
        throw new ArgumentNullException(message, new InvalidDataException());
    
    private static void CheckUrlsValidity(IEnumerable<string> urls)
    {
        if (urls.IsNullOrEmpty())
            return;

        foreach (var url in urls)
            CheckStringValidity(url);

        var pattern = new Regex(@"\bhttps?:\/\/\S+?\.(?:png|jpe?g)\b");
        
        if (urls.Any(url => !pattern.IsMatch(url)))
            throw new InvalidDataException("URL of the image has incorrect format!");
    }
}