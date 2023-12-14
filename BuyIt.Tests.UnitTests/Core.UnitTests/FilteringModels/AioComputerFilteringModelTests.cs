using System.Reflection;
using Application.FilteringModels;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.FilteringModels;

public class AioComputerFilteringModelTests
{
    [Fact]
    public void Constructor_InitializesPropertiesCorrectly()
    {
        var filteringModel = new AioComputerFilteringModel();

        Assert.NotNull(filteringModel.Category);
        Assert.Single(filteringModel.Category);
        Assert.Equal("All-in-one computer", filteringModel.Category[0]);
    }

    [Fact]
    public void Properties_InitializedAsLists()
    {
        var filteringModel = new AioComputerFilteringModel();

        Assert.NotNull(filteringModel.DisplayDiagonal);
        Assert.NotNull(filteringModel.DisplayResolution);
        Assert.NotNull(filteringModel.DisplayMatrixType);
        Assert.NotNull(filteringModel.DisplayRefreshRate);
    }
    
    [Fact]
    public void All_Properties_SetAndGetCorrectly()
    {
        var filteringModel = new AioComputerFilteringModel();
        var propertyValues = new Dictionary<string, List<string>>
        {
            { nameof(AioComputerFilteringModel.BrandName), new List<string> { "24 inches", "27 inches" } },
            { nameof(AioComputerFilteringModel.DisplayDiagonal), new List<string> { "24 inches", "27 inches" } },
            { nameof(AioComputerFilteringModel.DisplayResolution), new List<string> { "Full HD", "4K UHD" } },
            { nameof(AioComputerFilteringModel.DisplayMatrixType), new List<string> { "IPS", "LED" } },
            { nameof(AioComputerFilteringModel.DisplayRefreshRate), new List<string> { "60Hz", "120Hz" } },
            { nameof(AioComputerFilteringModel.Classification), new List<string> { "All-in-one computer", "AIO PC" } },
            { nameof(AioComputerFilteringModel.OperatingSystem), new List<string> { "Windows", "macOS" } },
            { nameof(AioComputerFilteringModel.ProcessorBrand), new List<string> { "Intel", "AMD" } },
            { nameof(AioComputerFilteringModel.ProcessorModel), new List<string> { "Core i9", "Ryzen 9" } },
            { nameof(AioComputerFilteringModel.ProcessorSeries), new List<string> { "10th Gen", "Zen 3" } },
            { nameof(AioComputerFilteringModel.CoresQuantity), new List<string> { "8", "12" } },
            { nameof(AioComputerFilteringModel.GraphicsCardBrand), new List<string> { "NVIDIA", "AMD" } },
            { nameof(AioComputerFilteringModel.GraphicsCardType), new List<string> { "Integrated", "Discrete" } },
            { nameof(AioComputerFilteringModel.GraphicsCardSeries), new List<string> { "GeForce", "Radeon" } },
            { nameof(AioComputerFilteringModel.GraphicsCardModel), new List<string> { "GTX 1660", "RX 5700" } },
            { nameof(AioComputerFilteringModel.GraphicsCardMemoryCapacity), new List<string> { "6GB", "8GB" } },
            { nameof(AioComputerFilteringModel.StorageType), new List<string> { "SSD", "HDD" } },
            { nameof(AioComputerFilteringModel.StorageCapacity), new List<string> { "512GB", "1TB" } },
            { nameof(AioComputerFilteringModel.RamType), new List<string> { "DDR4", "DDR4" } },
            { nameof(AioComputerFilteringModel.RamCapacity), new List<string> { "32GB", "64GB" } },
        };

        foreach (var entry in propertyValues)
        {
            PropertyInfo property = typeof(AioComputerFilteringModel).GetProperty(entry.Key)!;
            property.SetValue(filteringModel, Convert.ChangeType(entry.Value, property.PropertyType));
        }

        foreach (var entry in propertyValues)
        {
            PropertyInfo property = typeof(AioComputerFilteringModel).GetProperty(entry.Key)!;
            Assert.Equal(entry.Value, property.GetValue(filteringModel));
        }
    }
}