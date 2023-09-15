namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries
    .Common.FilteringModels.ComputerRelated;

public sealed class LaptopFilteringModel : PersonalComputerFilteringModel
{
    public LaptopFilteringModel() => Category = "Laptop";
    
    public string ModelFamily { get; set; }

    public string DisplayDiagonal { get; set; }

    public string DisplayResolution { get; set; }

    public string DisplayMatrixType { get; set; }

    public string DisplayRefreshRate { get; set; }
}