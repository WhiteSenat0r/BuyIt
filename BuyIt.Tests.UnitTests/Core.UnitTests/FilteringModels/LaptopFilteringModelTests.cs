using System.Reflection;
using Application.FilteringModels;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.FilteringModels;

public class LaptopFilteringModelTests
{
    [Fact]
    public void Constructor_InitializesPropertiesCorrectly()
    {
        var filteringModel = new LaptopFilteringModel();

        Assert.NotNull(filteringModel.Category);
        Assert.Single(filteringModel.Category);
        Assert.Equal("Laptop", filteringModel.Category[0]);
     }

    [Fact]
    public void Properties_InitializedAsLists()
    {
        var filteringModel = new LaptopFilteringModel();

        Assert.NotNull(filteringModel.ModelFamily);
        Assert.NotNull(filteringModel.DisplayDiagonal);
        Assert.NotNull(filteringModel.DisplayResolution);
        Assert.NotNull(filteringModel.DisplayMatrixType);
        Assert.NotNull(filteringModel.DisplayRefreshRate);
    }

    [Fact]
    public void All_MultipleChoiceProperties_SetAndGetCorrectly()
    {
        var filteringModel = new LaptopFilteringModel();
        var propertyValues = new Dictionary<string, List<string>>
        {
            { nameof(LaptopFilteringModel.ModelFamily), new List<string> { "Family1", "Family2" } },
            { nameof(LaptopFilteringModel.DisplayDiagonal), new List<string> { "14 inches", "15.6 inches" } },
            { nameof(LaptopFilteringModel.DisplayResolution), new List<string> { "Full HD", "4K UHD" } },
            { nameof(LaptopFilteringModel.DisplayMatrixType), new List<string> { "IPS", "OLED" } },
            { nameof(LaptopFilteringModel.DisplayRefreshRate), new List<string> { "60Hz", "120Hz" } },
            { nameof(LaptopFilteringModel.Classification), new List<string> { "Laptop", "Notebook" } },
            { nameof(LaptopFilteringModel.OperatingSystem), new List<string> { "Windows", "Linux" } },
            { nameof(LaptopFilteringModel.ProcessorBrand), new List<string> { "Intel", "AMD" } },
            { nameof(LaptopFilteringModel.ProcessorModel), new List<string> { "Core i5", "Ryzen 7" } },
            { nameof(LaptopFilteringModel.ProcessorSeries), new List<string> { "9th Gen", "Renoir" } },
            { nameof(LaptopFilteringModel.CoresQuantity), new List<string> { "6", "8" } },
            { nameof(LaptopFilteringModel.GraphicsCardBrand), new List<string> { "NVIDIA", "AMD" } },
            { nameof(LaptopFilteringModel.GraphicsCardType), new List<string> { "Integrated", "Discrete" } },
            { nameof(LaptopFilteringModel.GraphicsCardSeries), new List<string> { "GeForce", "Radeon" } },
            { nameof(LaptopFilteringModel.GraphicsCardModel), new List<string> { "GTX 1660", "RX 5600M" } },
            { nameof(LaptopFilteringModel.GraphicsCardMemoryCapacity), new List<string> { "6GB", "8GB" } },
            { nameof(LaptopFilteringModel.StorageType), new List<string> { "SSD", "HDD" } },
            { nameof(LaptopFilteringModel.StorageCapacity), new List<string> { "512GB", "1TB" } },
            { nameof(LaptopFilteringModel.RamType), new List<string> { "DDR4", "DDR5" } },
            { nameof(LaptopFilteringModel.RamCapacity), new List<string> { "16GB", "32GB" } },
        };

        foreach (var entry in propertyValues)
        {
            PropertyInfo property = typeof(LaptopFilteringModel).GetProperty(entry.Key);
            property.SetValue(filteringModel, Convert.ChangeType(entry.Value, property.PropertyType));
        }

        foreach (var entry in propertyValues)
        {
            PropertyInfo property = typeof(LaptopFilteringModel).GetProperty(entry.Key);
            Assert.Equal(entry.Value, property.GetValue(filteringModel));
        }
    }
}