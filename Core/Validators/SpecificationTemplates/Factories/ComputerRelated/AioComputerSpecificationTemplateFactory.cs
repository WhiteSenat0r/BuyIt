namespace Core.Validators.SpecificationTemplates.Factories.ComputerRelated;

internal class AioComputerSpecificationTemplateFactory : PersonalComputerSpecificationTemplateFactory
{
    // Current class creates a template, which is used for validation of all-in-one computer's
    // specifications. An example of specifications is described below: 
    // new Dictionary<string, IDictionary<string, string>>()
    // {
    //     {
    //         "Processor", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Manufacturer", "Apple"
    //             },
    //             {
    //                 "Series", "Apple M1"
    //             },
    //             {
    //                 "Model", "M1"
    //             },
    //             {
    //                 "Quantity of cores", "8"
    //             },
    //             {
    //                 "Quantity of threads", "8"
    //             },
    //             {
    //                 "Base clock", "2.1 Ghz"
    //             },
    //             {
    //                 "Max clock", "3.2 Ghz"
    //             },
    //             {
    //                 "Processor technology", "5 nm"
    //             }
    //         }
    //     },
    //     {
    //         "General", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Classification", "Premium"
    //             },
    //             {
    //                 "Operating system", "Mac OS"
    //             },
    //             {
    //                 "Main color", "Silver"
    //             }
    //         }
    //     },
    //     {
    //         "Graphics card", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Type", "Integrated"
    //             },
    //             {
    //                 "Manufacturer", "Apple"
    //             },
    //             {
    //                 "Model", "Apple M1"
    //             },
    //             {
    //                 "Series", "Apple M-X Graphics"
    //             },
    //             {
    //                 "Memory bus", "Dynamic"
    //             },
    //             {
    //                 "Type of memory", "Dynamic"
    //             },
    //             {
    //                 "Amount of memory", "Dynamic"
    //             }
    //         }
    //     },
    //     {
    //         "Storage", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Type", "SSD"
    //             },
    //             {
    //                 "Drive's interface", "PCI-ex SSD"
    //             },
    //             {
    //                 "Amount of memory", "256 GB"
    //             }
    //         }
    //     },
    //     {
    //         "Random access memory", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Type", "LPDDR4X"
    //             },
    //             {
    //                 "Amount of memory", "8 GB"
    //             }
    //         }
    //     },
    //     {
    //         "Measurements", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Width", "461 mm"
    //             },
    //             {
    //                 "Length", "147 mm"
    //             },
    //             {
    //                 "Depth", "547 mm"
    //             },
    //             {
    //                 "Weight", "4.46 Kg"
    //             }
    //         }
    //     },
    //     {
    //         "Interfaces and connection", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Network adapters", "Bluetooth, Ethernet, WiFi 802.11ax"
    //             },
    //             {
    //                 "Connectors and I/O ports", "Thunderbolt"
    //             }
    //         }
    //     },
    //     {
    //         "Display", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Diagonal", "24\""
    //             },
    //             {
    //                 "Resolution", "4480x2520"
    //             },
    //             {
    //                 "Matrix type", "IPS"
    //             },
    //             {
    //                 "Display type", "Regular"
    //             }
    //         }
    //     },
    //     {
    //         "Audio system", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Built-in microphone", "Present"
    //             },
    //             {
    //                 "Built-in speakers", "Present"
    //             }
    //         }
    //     },
    //     {
    //         "Additional", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Web-camera", "Present"
    //             },
    //             {
    //                 "Web-camera resolution", "1280x720 1.0 Mp"
    //             },
    //             {
    //                 "Mouse and keyboard", "Present"
    //             },
    //         }
    //     }
    // })
    
    internal override IDictionary<string, IEnumerable<string>> Create()
    {
        var template = base.Create();
        
        template["General"] = GetNewDictionaryValue
            (template, "General", new List<string> { "Model family", "Main color" });

        AddNewSpecificationToTemplate(template, "Display", new List<string>
        {
            "Diagonal", "Resolution", "Matrix type", "Display type"
        });
        
        AddNewSpecificationToTemplate(template, "Audio system", new List<string>
        {
            "Built-in microphone", "Built-in speakers"
        });

        return template;
    }
}