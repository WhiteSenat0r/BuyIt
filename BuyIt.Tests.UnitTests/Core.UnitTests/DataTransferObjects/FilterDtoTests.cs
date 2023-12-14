using Application.DataTransferObjects.ProductRelated.Specification;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.DataTransferObjects;

public class FilterDtoTests
{
    [Fact]
    public void Properties_SetAndGet_Success()
    {
        var filterDto = new FilterDto
        {
            MinPrice = 10,
            MaxPrice = 100,
            CountedBrands = new Dictionary<string, int> { { "Brand1", 5 }, { "Brand2", 10 } },
            CountedCategories = new Dictionary<string, int> { { "Category1", 8 }, { "Category2", 15 } },
            CountedSpecifications = new Dictionary<string, int> { { "Spec1", 3 }, { "Spec2", 7 } }
        };

        var minPrice = filterDto.MinPrice;
        var maxPrice = filterDto.MaxPrice;
        var countedBrands = filterDto.CountedBrands;
        var countedCategories = filterDto.CountedCategories;
        var countedSpecifications = filterDto.CountedSpecifications;

        Assert.Equal(10, minPrice);
        Assert.Equal(100, maxPrice);
        Assert.NotNull(countedBrands);
        Assert.NotNull(countedCategories);
        Assert.NotNull(countedSpecifications);
        Assert.Equal(5, countedBrands["Brand1"]);
        Assert.Equal(10, countedBrands["Brand2"]);
        Assert.Equal(8, countedCategories["Category1"]);
        Assert.Equal(15, countedCategories["Category2"]);
        Assert.Equal(3, countedSpecifications["Spec1"]);
        Assert.Equal(7, countedSpecifications["Spec2"]);
    }
}