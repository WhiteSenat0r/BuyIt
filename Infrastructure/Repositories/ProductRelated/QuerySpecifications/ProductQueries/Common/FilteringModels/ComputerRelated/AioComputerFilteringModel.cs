namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries
    .Common.FilteringModels.ComputerRelated;

public sealed class AioComputerFilteringModel : PersonalComputerFilteringModel
{
    public AioComputerFilteringModel() => Category = "All-in-one computer";
    
    public string DisplayDiagonal { get; set; }

    public string DisplayResolution { get; set; }

    public string DisplayMatrixType { get; set; }

    public string DisplayRefreshRate { get; set; }
}