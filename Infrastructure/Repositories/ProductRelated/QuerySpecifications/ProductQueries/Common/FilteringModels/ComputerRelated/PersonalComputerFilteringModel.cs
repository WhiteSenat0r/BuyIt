using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common
    .FilteringModels.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries
    .Common.FilteringModels.ComputerRelated;

public class PersonalComputerFilteringModel : BasicFilteringModel
{
    public PersonalComputerFilteringModel() => Category.Add("Personal computer");

    public List<string> Classification { get; set; } = new();

    public List<string> OperatingSystem { get; set; } = new();

    public List<string> ProcessorBrand { get; set; } = new();

    public List<string> ProcessorModel { get; set; } = new();

    public List<string> ProcessorSeries { get; set; } = new();

    public List<string> CoresQuantity { get; set; } = new();

    public List<string> GraphicsCardBrand { get; set; } = new();

    public List<string> GraphicsCardType { get; set; } = new();
    
    public List<string> GraphicsCardModel { get; set; } = new();

    public List<string> GraphicsCardSeries { get; set; } = new();

    public List<string> GraphicsCardMemoryCapacity { get; set; } = new();

    public List<string> StorageType { get; set; } = new();

    public List<string> StorageCapacity { get; set; } = new();

    public List<string> RamType { get; set; } = new();

    public List<string> RamCapacity { get; set; } = new();
}