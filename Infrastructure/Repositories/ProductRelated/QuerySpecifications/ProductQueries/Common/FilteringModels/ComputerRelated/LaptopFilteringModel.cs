namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries
    .Common.FilteringModels.ComputerRelated;

public sealed class LaptopFilteringModel : PersonalComputerFilteringModel
{
    public LaptopFilteringModel() => Category.Add("Laptop");

    public List<string> ModelFamily { get; set; } = new();

    public List<string> DisplayDiagonal { get; set; } = new();

    public List<string> DisplayResolution { get; set; } = new();

    public List<string> DisplayMatrixType { get; set; } = new();

    public List<string> DisplayRefreshRate { get; set; } = new();
}