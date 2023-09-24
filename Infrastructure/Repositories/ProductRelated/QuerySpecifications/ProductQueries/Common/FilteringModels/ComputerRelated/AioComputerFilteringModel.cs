namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries
    .Common.FilteringModels.ComputerRelated;

public sealed class AioComputerFilteringModel : PersonalComputerFilteringModel
{
    public AioComputerFilteringModel() => Category.Add("All-in-one computer");
    
    public List<string> DisplayDiagonal { get; set; } = new();

    public List<string> DisplayResolution { get; set; } = new();

    public List<string> DisplayMatrixType { get; set; } = new();

    public List<string> DisplayRefreshRate { get; set; } = new();
}