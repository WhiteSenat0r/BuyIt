using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Core.Builders.PathBuilders;
using Core.Entities.Product.Common.Interfaces;
using Core.Validators;
using Microsoft.IdentityModel.Tokens;

namespace Core.Entities.Product;

public class Product : IProduct
{
    private string _name = null!;
    private string _description = null!;
    private decimal _price;
    private IEnumerable<string> _mainImagesNames = null!;
    private IDictionary<string, IDictionary<string, string>> _specifications;

    public Product() => ProductCode = Id.ToString()[..8].ToUpper(); // Required by EF Core for object's initialization from database

    public Product // Typically used in non-database initialization
    (
        string name,
        string description,
        decimal price,
        bool inStock,
        ProductManufacturer manufacturer,
        ProductType productType,
        ProductRating rating,
        IEnumerable<string> mainImagesNames,
        IDictionary<string, IDictionary<string, string>> specifications)
    {
        Name = name;
        Description = description;
        Price = price;
        InStock = inStock;
        ProductCode = Id.ToString()[..8].ToUpper();
        Manufacturer = manufacturer;
        ManufacturerId = manufacturer.Id;
        ProductType = productType;
        ProductTypeId = productType.Id;
        RatingId = rating.Id;
        MainImagesNames = mainImagesNames;
        Specifications = specifications;
        Manufacturer = null!;
        ProductType = null!;
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
            (value, ref _description, false);
    }

    [Column(TypeName = "decimal(8, 2)")]
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
    
    [MaxLength(8)]
    public string ProductCode { get; set; } = null!; // Description of the product

    public IEnumerable<string> MainImagesNames                                        
    {
        get => _mainImagesNames;
        set
        {
            if (value.IsNullOrEmpty())
                ThrowArgumentNullException("Main images must be present!");
            CheckFileFormatValidity(value);
            value = ImagePathBuilder.Build(value, ProductType, Manufacturer, ProductCode);
            _mainImagesNames = value;
        }
    }

    public IDictionary<string, IDictionary<string, string>> Specifications
    {
        get => _specifications;
        set
        {
            if (value.IsNullOrEmpty()) 
                ThrowArgumentNullException("Specifications can not be null!");
            var validator = new ProductSpecificationValidator(ProductType.Name, value);
            validator.Validate();
            _specifications = value;
        }
    }

    private static void AssignStringValue
        (string text, ref string assignedVariable, bool isName = true)
    {
        CheckStringValidity(text);
        
        var maxLengthAttribute = GetSuitableMaxLengthAttribute(isName);

        if (maxLengthAttribute is not null &&
            maxLengthAttribute.Length < text.Length)
            throw new ArgumentException("String's length is greater that maximum allowed length!");
            
        assignedVariable = text;
    }

    private static MaxLengthAttribute GetSuitableMaxLengthAttribute(bool isName)
    {
        var propertyInfo = isName ? typeof(Product).GetProperty("Name") 
            : typeof(Product).GetProperty("Description");

        return (MaxLengthAttribute)Attribute.GetCustomAttribute
            (propertyInfo!, typeof(MaxLengthAttribute))!;
    }

    private static void CheckStringValidity(string text)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)) 
            ThrowArgumentNullException
                ("String is null, empty or consists only of white spaces!");
    }

    private static void ThrowArgumentNullException(string message) =>
        throw new ArgumentNullException(message, new InvalidDataException());
    
    private static void CheckFileFormatValidity(IEnumerable<string> fileNames)
    {
        if (fileNames.IsNullOrEmpty())
            return;

        foreach (var fileName in fileNames)
            CheckStringValidity(fileName);

        if (fileNames.Distinct().Count() != fileNames.Count())
            throw new ArgumentException("Some files have an identical name!");

        var pattern = new Regex(@"\.(jpe?g|png|webp|bmp)$");
        
        if (fileNames.Any(url => !pattern.IsMatch(url)))
            throw new InvalidDataException("Name of the image has incorrect format!");
    }
}