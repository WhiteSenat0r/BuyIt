using Core.Entities.Product;
using Core.Entities.Product.Common.Interfaces;
using Core.Validators.Interfaces;
using Core.Validators.SpecificationTemplates.Factories.ComputerRelated;
using Microsoft.IdentityModel.Tokens;

namespace Core.Validators;

public class ProductSpecificationValidator : IValidator
{
    private readonly IDictionary<string, IDictionary<string, string>>
        _productSpecifications = null!;
    
    private readonly IProductType _givenProductType = null!;
    
    public ProductSpecificationValidator(string productTypeName,
        IDictionary<string, IDictionary<string, string>> specifications)
    {
        GivenProductType = new ProductType(productTypeName);
        ProductSpecifications = specifications;
        RequiredSpecifications = GetRequiredSpecificationTemplate(GivenProductType);
    }

    private IProductType GivenProductType
    {
        get => _givenProductType;
        init
        {
            if (value is null || string.IsNullOrEmpty(value.Name) || string.IsNullOrWhiteSpace(value.Name))
                ThrowArgumentNullException("Given product type is null or empty!");
            _givenProductType = value;
        }
    }
    
    private IDictionary<string, IEnumerable<string>> RequiredSpecifications { get; }

    private IDictionary<string, IDictionary<string, string>>
        ProductSpecifications
    {
        get => _productSpecifications;
        init => _productSpecifications = 
            value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public void Validate()
    {
        CheckSpecificationsForNullOrEmptyValues();
        CheckProductSpecificationTemplateMatching();
    }

    private void CheckProductSpecificationTemplateMatching()
    {
        CheckProductSpecificationForRedundantAttributes();
        CheckAllAttributesPresence();
    }

    private void CheckAllAttributesPresence()
    {
        foreach (var specification in RequiredSpecifications)
        {
            if (!ProductSpecifications.ContainsKey(specification.Key))
                throw new ArgumentException(
                    @$"""{specification.Key}"" specification is not present in the given product specification!");

            ProductSpecifications.TryGetValue
                (specification.Key, out var productSpecificationAttributes);

            if (productSpecificationAttributes!.Keys.All(attribute => specification.Value.Contains(attribute)))
                continue;
            var keys = specification.Value
                .Where(item => !productSpecificationAttributes.ContainsKey(item))
                .Select(item => item)
                .ToList();
            throw new ArgumentException(
                @$"""{keys.First()}"" attribute key is not present in the given ""{specification.Key}"" specification!");
        }
    }

    private void CheckProductSpecificationForRedundantAttributes()
    {
        foreach (var specification in ProductSpecifications)
            if (!RequiredSpecifications.ContainsKey(specification.Key) &&
                !specification.Key.ToLower().Equals("additional"))
                throw new ArgumentException(@$"""{specification.Key}"" 
                    specification attribute must not be present in current product specifications!");
    }

    private void CheckSpecificationsForNullOrEmptyValues()
    {
        if (ProductSpecifications.Keys.Any(string.IsNullOrEmpty) ||
            ProductSpecifications.Keys.Any(string.IsNullOrWhiteSpace))
            ThrowArgumentNullException
                (@"One of specifications' keys is null, empty or consists only of whitespaces!");

        if (ProductSpecifications.Values.Any(specification => specification.IsNullOrEmpty()))
            ThrowArgumentNullException("One of specifications' values is null or empty!");

        if (ProductSpecifications.Values.Any(attribute => attribute.Keys.Any(string.IsNullOrEmpty)
                                                          || attribute.Keys.Any(string.IsNullOrWhiteSpace)))
            ThrowArgumentNullException
                ("One of attributes' keys is null, empty or consists only of whitespaces!");

        if (ProductSpecifications.Values.Any(attribute => attribute.Values.Any(string.IsNullOrEmpty)
                                                          || attribute.Values.Any(string.IsNullOrWhiteSpace)))
            ThrowArgumentNullException
                ("One of attributes' keys is null, empty or consists only of whitespaces!");
    }

    private static IDictionary<string, IEnumerable<string>> GetRequiredSpecificationTemplate
        (IProductType productType) => productType.Name switch
    {
        "Laptop" => new LaptopSpecificationTemplateFactory().Create(),
        "All-in-one computer" => new AioComputerSpecificationTemplateFactory().Create(),
        "Personal computer" => new PersonalComputerSpecificationTemplateFactory().Create(),
        _ => throw new ArgumentException("Invalid product type was received!")
    };
    
    private static void ThrowArgumentNullException(string message) =>
        throw new ArgumentNullException(message, new InvalidDataException());
}