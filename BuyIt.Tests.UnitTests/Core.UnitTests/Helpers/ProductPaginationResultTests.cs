using Application.Contracts;
using Application.DataTransferObjects.ProductRelated;
using Application.FilteringModels;
using Application.Helpers;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.Helpers;

public class ProductPaginationResultTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        var items = new List<IProductDto>
        {
            new FullProductDto
            {
                Name = "Product 1", Description = "Description 1", Price = 10.0m, InStock = "Yes", Rating = 4.5,
                ProductCode = "ABC123", Images = new List<string> { "image1.jpg", "image2.jpg" }
            },
            new FullProductDto
            {
                Name = "Product 2", Description = "Description 2", Price = 15.0m, InStock = "No", Rating = 3.0,
                ProductCode = "XYZ789", Images = new List<string> { "image3.jpg" }
            },
            new FullProductDto
            {
                Name = "Product 3", Description = "Description 3", Price = 20.0m, InStock = "Yes", Rating = null,
                ProductCode = "123XYZ", Images = new List<string>()
            }
        };
        
        var filteringModel = new PersonalComputerFilteringModel() { PageIndex = 1 };

        var paginationResult = new ProductPaginationResult(items, filteringModel, items.Count);

        Assert.Equal(items, paginationResult.Items);
        Assert.Equal(items.Count, paginationResult.CurrentPageItemsQuantity);
        Assert.Equal(filteringModel.PageIndex, paginationResult.PageIndex);
    }
}