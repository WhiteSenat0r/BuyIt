﻿using System.Text;
using Core.Entities.Product.Common.Interfaces;

namespace Core.Builders.PathBuilders;

public static class ImagePathBuilder
{
    public static IEnumerable<string> Build
        (IEnumerable<string> inputPaths, IProductType productType,
            IProductManufacturer productManufacturer, string productCode)
    {
        var result = new List<string>();
        
        var pathBuilder = new StringBuilder();

        var categoryNameEnding = GetCategoryNameEnding(productType);

        foreach (var path in inputPaths)
        {
            AppendCategoryToPath(categoryNameEnding, pathBuilder, productType);
            AppendBrandToPath(productManufacturer, pathBuilder);
            AppendProductCode(productCode, pathBuilder);
            result.Add(pathBuilder.Append(path).ToString());
            pathBuilder.Clear();
        }
        
        return result;
    }

    private static void AppendProductCode(string productCode, StringBuilder pathBuilder) =>
        pathBuilder.Append(productCode.ToLower() + '/');

    private static void AppendBrandToPath
        (IProductManufacturer productManufacturer, StringBuilder pathBuilder) =>
        pathBuilder.Append(productManufacturer.Name.ToLower() + '/');

    private static void AppendCategoryToPath(
        string categoryNameEnding, StringBuilder pathBuilder, IProductType category)
    {
        var vowels = new[] { 'a', 'e', 'i', 'o', 'u' };
        
        if (categoryNameEnding.Equals("es"))
            pathBuilder.Append(category.Name.ToLower());
        else if (categoryNameEnding[^1].Equals('s') 
                 || categoryNameEnding[^1].Equals('x') || categoryNameEnding[^1].Equals('z')
                 || categoryNameEnding.Equals("ch") || categoryNameEnding.Equals("sh")
                 || vowels.Any(c => c.Equals
                     (categoryNameEnding[0]) && categoryNameEnding[^1].Equals('o')))
            pathBuilder.Append(category.Name.ToLower() + 'e' + 's');
        else if (vowels.Any(c => c.Equals(categoryNameEnding[^1])) &&
                 categoryNameEnding[categoryNameEnding[0]].Equals('f'))
            pathBuilder.Append(category.Name.ToLower().Replace
                ($"f{categoryNameEnding[^1]}", $"v{categoryNameEnding[^1]}"));
        else 
            pathBuilder.Append(category.Name.ToLower() + 's');
        pathBuilder.Append('/');
    }

    private static string GetCategoryNameEnding(IProductType productType) =>
        productType.Name.Substring(productType.Name.Length - 2, 2).ToLower();
}