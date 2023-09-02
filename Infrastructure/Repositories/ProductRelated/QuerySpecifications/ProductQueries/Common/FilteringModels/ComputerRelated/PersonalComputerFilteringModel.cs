using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common
    .FilteringModels.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries
    .Common.FilteringModels.ComputerRelated;

public class PersonalComputerFilteringModel : BasicProductFilteringModel
{
    public PersonalComputerFilteringModel() => Category = "Personal computer";
    
    public string Classification { get; set; }

    public string OperatingSystem { get; set; }

    public string ProcessorBrand { get; set; }
    
    public string ProcessorModel { get; set; }

    public string ProcessorSeries { get; set; }

    public string CoresQuantity { get; set; }

    public string GraphicsCardBrand { get; set; }

    public string GraphicsCardType { get; set; }
    
    public string GraphicsCardModel { get; set; }

    public string GraphicsCardSeries { get; set; }

    public string GraphicsCardMemoryCapacity { get; set; }

    public string StorageType { get; set; }

    public string StorageCapacity { get; set; }

    public string RamType { get; set; }

    public string RamCapacity { get; set; }
}