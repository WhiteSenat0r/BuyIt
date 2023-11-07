using Application.Contracts;
using Domain.Contracts.ProductRelated;

namespace Application.Helpers;

internal class ComputerShortDescription : IShortDescription
{
    public string GetShortDescription(IProduct product) =>
        product.ProductType.Name switch
        {
            "Laptop" => GetLaptopShortDescription(product),
            "All-in-one computer" => GetAllInOneComputerShortDescription(product),
            _ => GetPersonalComputerShortDescription(product)
        };

    private string GetAllInOneComputerShortDescription(IProduct product) =>
        GetLaptopShortDescription(product) 
        + $" | OS: {product.Specifications.Single(s => s.SpecificationCategory.Value.Equals
            ("General") && s.SpecificationAttribute.Value.Equals("Operating system")).SpecificationValue.Value}";

    private string GetLaptopShortDescription(IProduct product) =>
        $"Display: {product.Specifications.Single(s => s.SpecificationCategory.Value.Equals
            ("Display") && s.SpecificationAttribute.Value.Equals("Diagonal")).SpecificationValue.Value} | " +
        GetPersonalComputerShortDescription(product);

    private string GetPersonalComputerShortDescription(IProduct product) =>
        $"CPU: {product.Specifications.Single(s => s.SpecificationCategory.Value.Equals
            ("Processor") && s.SpecificationAttribute.Value.Equals("Model")).SpecificationValue.Value} | " +
        $"GPU: {product.Specifications.Single(s => s.SpecificationCategory.Value.Equals
            ("Graphics card") && s.SpecificationAttribute.Value.Equals("Model")).SpecificationValue.Value} | " +
        $"RAM: {product.Specifications.Single(s => s.SpecificationCategory.Value.Equals
            ("Random access memory") && s.SpecificationAttribute.Value.Equals("Amount of memory")).SpecificationValue.Value} | " +
        $"ROM: {product.Specifications.Single(s => s.SpecificationCategory.Value.Equals
            ("Storage") && s.SpecificationAttribute.Value.Equals("Amount of memory")).SpecificationValue.Value}";
}