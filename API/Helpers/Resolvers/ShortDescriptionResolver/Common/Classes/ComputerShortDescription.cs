using API.Helpers.Resolvers.ShortDescriptionResolver.Common.Interfaces;
using Core.Entities.Product.Common.Interfaces;

namespace API.Helpers.Resolvers.ShortDescriptionResolver.Common.Classes;

internal class ComputerShortDescription : IShortDescription
{
    public string GetShortDescription(IProduct product) =>
        product.ProductType.Name switch
        {
            "Laptop" => GetLaptopShortDescription(product),
            "All-in-one computer" => GetAllInOneComputerShortDescription(product),
            _ => GetPersonalComputerShortDescription(product)
        };

    private static string GetAllInOneComputerShortDescription(IProduct product) =>
        GetPersonalComputerShortDescription(product) 
        + $" | OS: {product.Specifications.Single(s => s.Category.Equals
            ("General") && s.Attribute.Equals("Operating system")).Value}";

    private static string GetLaptopShortDescription(IProduct product) =>
        $"Display: {product.Specifications.Single(s => s.Category.Equals
            ("Display") && s.Attribute.Equals("Diagonal")).Value} | " +
        GetPersonalComputerShortDescription(product);

    private static string GetPersonalComputerShortDescription(IProduct product) =>
        $"CPU: {product.Specifications.Single(s => s.Category.Equals
            ("Processor") && s.Attribute.Equals("Model")).Value} | " +
        $"GPU: {product.Specifications.Single(s => s.Category.Equals
            ("Graphics card") && s.Attribute.Equals("Model")).Value} | " +
        $"RAM: {product.Specifications.Single(s => s.Category.Equals
            ("Random access memory") && s.Attribute.Equals("Amount of memory")).Value} | " +
        $"ROM: {product.Specifications.Single(s => s.Category.Equals
            ("Storage") && s.Attribute.Equals("Amount of memory")).Value}";
}