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

    protected override IDictionary<string, IDictionary<string, string>> GetMappedFilterNamings()
    {
        var namings = new Dictionary<string, IDictionary<string, string>>
        {
            {
                nameof(ModelFamily), new Dictionary<string, string>
                {
                    {
                        "General", "Model family"
                    }
                }
            },
            {
                nameof(DisplayDiagonal), new Dictionary<string, string>
                {
                    {
                        "Display", "Diagonal"
                    }
                }
            },
            {
                nameof(DisplayResolution), new Dictionary<string, string>
                {
                    {
                        "Display", "Resolution"
                    }
                }
            },
            {
                nameof(DisplayResolution), new Dictionary<string, string>
                {
                    {
                        "Display", "Resolution"
                    }
                }
            },
            {
                nameof(DisplayMatrixType), new Dictionary<string, string>
                {
                    {
                        "Display", "Matrix type"
                    }
                }
            },
            {
                nameof(DisplayRefreshRate), new Dictionary<string, string>
                {
                    {
                        "Display", "Refresh rate"
                    }
                }
            }
        };

        foreach (var element in base.GetMappedFilterNamings())
            namings.Add(element.Key, (Dictionary<string, string>)element.Value);

        return namings;
    }
}