using Application.Specifications;

namespace Application.FilteringModels;

public sealed class LaptopFilteringModel : PersonalComputerFilteringModel
{
    public LaptopFilteringModel()
    {
        Category.Add("Laptop");
        QuerySpecificationMapping
            [typeof(LaptopFilteringModel)] = typeof(LaptopQuerySpecification);
    }

    public List<string> ModelFamily { get; set; } = new();

    public List<string> DisplayDiagonal { get; set; } = new();

    public List<string> DisplayResolution { get; set; } = new();

    public List<string> DisplayMatrixType { get; set; } = new();

    public List<string> DisplayRefreshRate { get; set; } = new();
}