using Application.Specifications.ProductSpecifications.ComputerRelatedSpecifications;

namespace Application.FilteringModels;

public class PersonalComputerFilteringModel : BasicFilteringModel
{
    public PersonalComputerFilteringModel()
    {
        Category = new List<string> { "Personal computer" };
        
        QuerySpecificationMapping
            [typeof(PersonalComputerFilteringModel)] = typeof(PersonalComputerQuerySpecification);
    }

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

    protected override IDictionary<string, IDictionary<string, string>> GetMappedFilterNamings() =>
        new Dictionary<string, IDictionary<string, string>>
        {
            {
                nameof(Classification), new Dictionary<string, string>
                {
                    {
                        "General", "Classification"
                    }
                }
            },
            {
                nameof(OperatingSystem), new Dictionary<string, string>
                {
                    {
                        "General", "Operating system"
                    }
                }
            },
            {
                nameof(ProcessorBrand), new Dictionary<string, string>
                {
                    {
                        "Processor", "Manufacturer"
                    }
                }
            },
            {
                nameof(ProcessorModel), new Dictionary<string, string>
                {
                    {
                        "Processor", "Model"
                    }
                }
            },
            {
                nameof(ProcessorSeries), new Dictionary<string, string>
                {
                    {
                        "Processor", "Series"
                    }
                }
            },
            {
                nameof(CoresQuantity), new Dictionary<string, string>
                {
                    {
                        "Processor", "Quantity of cores"
                    }
                }
            },
            {
                nameof(GraphicsCardBrand), new Dictionary<string, string>
                {
                    {
                        "Graphics card", "Manufacturer"
                    }
                }
            },
            {
                nameof(GraphicsCardType), new Dictionary<string, string>
                {
                    {
                        "Graphics card", "Type"
                    }
                }
            },
            {
                nameof(GraphicsCardSeries), new Dictionary<string, string>
                {
                    {
                        "Graphics card", "Series"
                    }
                }
            },
            {
                nameof(GraphicsCardModel), new Dictionary<string, string>
                {
                    {
                        "Graphics card", "Model"
                    }
                }
            },
            {
                nameof(GraphicsCardMemoryCapacity), new Dictionary<string, string>
                {
                    {
                        "Graphics card", "Amount of memory"
                    }
                }
            },
            {
                nameof(StorageType), new Dictionary<string, string>
                {
                    {
                        "Storage", "Type"
                    }
                }
            },
            {
                nameof(StorageCapacity), new Dictionary<string, string>
                {
                    {
                        "Storage", "Amount of memory"
                    }
                }
            },
            {
                nameof(RamType), new Dictionary<string, string>
                {
                    {
                        "Random access memory", "Type"
                    }
                }
            },
            {
                nameof(RamCapacity), new Dictionary<string, string>
                {
                    {
                        "Random access memory", "Amount of memory"
                    }
                }
            },
        };
}