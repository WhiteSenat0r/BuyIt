using System.Reflection;
using Application.FilteringModels;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.FilteringModels;

using System;
using System.Collections.Generic;
using Xunit;

public class PersonalComputerFilteringModelTests
{
    [Fact]
    public void Constructor_InitializesPropertiesCorrectly()
    {
        var filteringModel = new PersonalComputerFilteringModel();

        Assert.NotNull(filteringModel.Category);
        Assert.Single(filteringModel.Category);
        Assert.Equal("Personal computer", filteringModel.Category[0]);
    }

    [Fact]
    public void Properties_InitializedAsLists()
    {
        var filteringModel = new PersonalComputerFilteringModel();

        Assert.NotNull(filteringModel.Classification);
        Assert.NotNull(filteringModel.OperatingSystem);
        Assert.NotNull(filteringModel.ProcessorBrand);
        Assert.NotNull(filteringModel.ProcessorModel);
        Assert.NotNull(filteringModel.ProcessorSeries);
        Assert.NotNull(filteringModel.CoresQuantity);
        Assert.NotNull(filteringModel.GraphicsCardBrand);
        Assert.NotNull(filteringModel.GraphicsCardType);
        Assert.NotNull(filteringModel.GraphicsCardSeries);
        Assert.NotNull(filteringModel.GraphicsCardModel);
        Assert.NotNull(filteringModel.GraphicsCardMemoryCapacity);
        Assert.NotNull(filteringModel.StorageType);
        Assert.NotNull(filteringModel.StorageCapacity);
        Assert.NotNull(filteringModel.RamType);
        Assert.NotNull(filteringModel.RamCapacity);
    }
    
    [Fact]
    public void All_MultipleChoiceProperties_SetAndGetCorrectly()
    {
        var filteringModel = new PersonalComputerFilteringModel();
        var propertyValues = new Dictionary<string, List<string>>
        {
            { nameof(PersonalComputerFilteringModel.Classification), new List<string> { "Laptop", "Desktop" } },
            { nameof(PersonalComputerFilteringModel.OperatingSystem), new List<string> { "Windows", "Linux" } },
            { nameof(PersonalComputerFilteringModel.ProcessorBrand), new List<string> { "Intel", "AMD" } },
            { nameof(PersonalComputerFilteringModel.ProcessorModel), new List<string> { "Core i7", "Ryzen 5" } },
            { nameof(PersonalComputerFilteringModel.ProcessorSeries), new List<string> { "10th Gen", "Zen 2" } },
            { nameof(PersonalComputerFilteringModel.CoresQuantity), new List<string> { "4", "8" } },
            { nameof(PersonalComputerFilteringModel.GraphicsCardBrand), new List<string> { "NVIDIA", "AMD" } },
            { nameof(PersonalComputerFilteringModel.GraphicsCardType), new List<string> { "Integrated", "Discrete" } },
            { nameof(PersonalComputerFilteringModel.GraphicsCardSeries), new List<string> { "GeForce", "Radeon" } },
            { nameof(PersonalComputerFilteringModel.GraphicsCardModel), new List<string> { "GTX 1650", "RX 5700" } },
            { nameof(PersonalComputerFilteringModel.GraphicsCardMemoryCapacity), new List<string> { "4GB", "8GB" } },
            { nameof(PersonalComputerFilteringModel.StorageType), new List<string> { "SSD", "HDD" } },
            { nameof(PersonalComputerFilteringModel.StorageCapacity), new List<string> { "256GB", "1TB" } },
            { nameof(PersonalComputerFilteringModel.RamType), new List<string> { "DDR4", "DDR5" } },
            { nameof(PersonalComputerFilteringModel.RamCapacity), new List<string> { "8GB", "16GB" } },
        };
        
        foreach (var entry in propertyValues)
        {
            PropertyInfo property = typeof(PersonalComputerFilteringModel).GetProperty(entry.Key);
            property.SetValue(filteringModel, Convert.ChangeType(entry.Value, property.PropertyType));
        }

        foreach (var entry in propertyValues)
        {
            PropertyInfo property = typeof(PersonalComputerFilteringModel).GetProperty(entry.Key);
            Assert.Equal(entry.Value, property.GetValue(filteringModel));
        }
    }
}