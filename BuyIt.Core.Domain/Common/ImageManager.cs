using System.Text;
using Domain.Contracts.ProductRelated;

namespace Domain.Common;

public sealed class ImageManager
{
    private readonly string _baseDirectory;
    private readonly string _contentDirectoryFragment;

    public ImageManager()
    {
        _baseDirectory = GetBaseDirectory();
        _contentDirectoryFragment = GetContentDirectoryFragment();
    }
    
    public IEnumerable<string> BuildImagePaths
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

    public void CreateProductImageDirectory(string imagePath)
    {
        Directory.CreateDirectory(
            _baseDirectory + _contentDirectoryFragment + imagePath[..imagePath.LastIndexOf('/')]);
    }
    
    private string GetBaseDirectory()
    {
        var currentDomainDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var splitIndex = currentDomainDirectory.LastIndexOf("BuyIt.Domain\\") + "BuyIt.Domain\\".Length;
        
        return currentDomainDirectory[..splitIndex].Replace('\\', '/');
    }
    
    private string GetContentDirectoryFragment() => 
        "BuyIt.Presentation.WebAPI/wwwroot/content/products/images/";

    private void AppendProductCode(string productCode, StringBuilder pathBuilder) =>
        pathBuilder.Append(productCode.ToLower() + '/');

    private void AppendBrandToPath
        (IProductManufacturer productManufacturer, StringBuilder pathBuilder) =>
        pathBuilder.Append(productManufacturer.Name.ToLower() + '/');

    private void AppendCategoryToPath(
        string categoryNameEnding, StringBuilder pathBuilder, IProductType category)
    {
        var vowels = new[] { 'a', 'e', 'i', 'o', 'u' };

        var categoryName = category.Name;

        if (categoryName.Contains(' '))
            categoryName = categoryName.Replace(' ', '-');
        
        DeterminateCategoryEnding(categoryNameEnding, pathBuilder, categoryName, vowels);
        
        pathBuilder.Append('/');
    }

    private void DeterminateCategoryEnding
        (string categoryNameEnding, StringBuilder pathBuilder, string categoryName, char[] vowels)
    {
        if (categoryNameEnding.Equals("es"))
            pathBuilder.Append(categoryName.ToLower());
        else if (categoryNameEnding[^1].Equals('s')
                 || categoryNameEnding[^1].Equals('x') || categoryNameEnding[^1].Equals('z')
                 || categoryNameEnding.Equals("ch") || categoryNameEnding.Equals("sh")
                 || vowels.Any(c => c.Equals
                     (categoryNameEnding[0]) && categoryNameEnding[^1].Equals('o')))
            pathBuilder.Append(categoryName.ToLower() + 'e' + 's');
        else if (vowels.Any(c => c.Equals(categoryNameEnding[^1])) &&
                 categoryNameEnding[categoryNameEnding[0]].Equals('f'))
            pathBuilder.Append(categoryName.ToLower().Replace
                ($"f{categoryNameEnding[^1]}", $"v{categoryNameEnding[^1]}"));
        else
            pathBuilder.Append(categoryName.ToLower() + 's');
    }

    private string GetCategoryNameEnding(IProductType productType) =>
        productType.Name.Substring(productType.Name.Length - 2, 2).ToLower();
}