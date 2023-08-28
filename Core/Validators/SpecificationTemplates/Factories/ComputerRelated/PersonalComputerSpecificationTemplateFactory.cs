using Core.Validators.SpecificationTemplates.Factories.Common;

namespace Core.Validators.SpecificationTemplates.Factories.ComputerRelated;

internal class PersonalComputerSpecificationTemplateFactory
    : SpecificationTemplateFactory
{
    internal override IDictionary<string, IEnumerable<string>> Create() =>
        new Dictionary<string, IEnumerable<string>>
        {
            {
                "General", new List<string>
                {
                    "Classification",
                    "Operating system"
                }
            },
            {
                "Processor", new List<string>
                {
                    "Manufacturer",
                    "Model",
                    "Series",
                    "Quantity of cores",
                    "Quantity of threads",
                    "Base clock",
                    "Max clock",
                    "Processor technology"
                }
            },
            {
                "Graphics card", new List<string>
                {
                    "Type",
                    "Manufacturer",
                    "Model",
                    "Series",
                    "Memory bus",
                    "Type of memory",
                    "Amount of memory"
                }
            },
            {
                "Storage", new List<string>
                {
                    "Type",
                    "Drive's interface",
                    "Amount of memory"
                }
            },
            {
                "Random access memory", new List<string>
                {
                    "Type",
                    "Amount of memory"
                }
            },
            {
                "Interfaces and connection", new List<string>
                {
                    "Network adapters",
                    "Connectors and I/O ports"
                }
            },
            {
                "Measurements", new List<string>
                {
                    "Width",
                    "Depth",
                    "Length",
                    "Weight"
                }
            }
        };
}