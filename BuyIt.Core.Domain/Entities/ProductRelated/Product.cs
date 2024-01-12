using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Domain.Common;
using Domain.Contracts.ProductRelated;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Entities.ProductRelated;

public sealed class Product : IProduct
{
    private string _name = null!;
    private string _description = null!;
    private decimal _price;
    private IEnumerable<string> _mainImagesNames = null!;

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
        IEnumerable<string> mainImagesNames)
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
        Manufacturer = null!;
        ProductType = null!;
    }
    
    public Guid Id { get; set; } = Guid.NewGuid(); // Id of the product
    
    public string Name // Name of the product
    {
        get => _name;
        set => AssignStringValue
            (value, ref _name!);
    }
    
    [MaxLength(2048)]
    public string Description // Description of the product
    {
        get => _description;
        set => AssignStringValue
            (value, ref _description!, false);
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
    public string ProductCode { get; set; }

    public IEnumerable<string> MainImagesNames                                        
    {
        get => _mainImagesNames;
        set
        {
            if (value.IsNullOrEmpty())
                ThrowArgumentNullException("Main images must be present!");
            CheckFileFormatValidity(value);
            var imageManager = new ImageManager();
            value = imageManager.BuildImagePaths(value, ProductType, Manufacturer, ProductCode);
            imageManager.CreateProductImageDirectory(value.First());
            _mainImagesNames = value;
        }
    }

    public ICollection<ProductSpecification> Specifications { get; set; } = new List<ProductSpecification>();

    private void AssignStringValue
        (string text, ref string assignedVariable, bool isName = true)
    {
        CheckStringValidity(text);
        
        var maxLengthAttribute = GetSuitableMaxLengthAttribute(isName);

        if (maxLengthAttribute is not null &&
            maxLengthAttribute.Length < text.Length)
            throw new ArgumentException("String's length is greater that maximum allowed length!");
            
        assignedVariable = text;
    }

    private MaxLengthAttribute GetSuitableMaxLengthAttribute(bool isName)
    {
        var propertyInfo = isName ? typeof(Product).GetProperty("Name") 
            : typeof(Product).GetProperty("Description");

        return (MaxLengthAttribute)Attribute.GetCustomAttribute
            (propertyInfo!, typeof(MaxLengthAttribute))!;
    }

    private void CheckStringValidity(string text)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)) 
            ThrowArgumentNullException
                ("String is null, empty or consists only of white spaces!");
    }

    private void ThrowArgumentNullException(string message) =>
        throw new ArgumentNullException(message, new InvalidDataException());
    
    private void CheckFileFormatValidity(IEnumerable<string> fileNames)
    {
        if (fileNames.IsNullOrEmpty())
            return;

        foreach (var fileName in fileNames)
            CheckStringValidity(fileName);

        if (fileNames.Distinct().Count() != fileNames.Count())
            throw new ArgumentException("Some files have an identical name!");

        var pattern = new Regex(@"\.(jpg)$");
        
        if (fileNames.Any(url => !pattern.IsMatch(url)))
            throw new InvalidDataException("Name of the image has incorrect format!");
    }
}