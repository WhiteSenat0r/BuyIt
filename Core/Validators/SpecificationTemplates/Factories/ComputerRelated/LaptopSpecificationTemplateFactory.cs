namespace Core.Validators.SpecificationTemplates.Factories.ComputerRelated;

internal class LaptopSpecificationTemplateFactory : PersonalComputerSpecificationTemplateFactory
{
    // Current class creates a template, which is used for validation of laptop's
    // specifications. An example of specifications is described below: 
    // new Dictionary<string, IDictionary<string, string>>()
    // {
    //     {
    //         "Processor", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Manufacturer", "Intel"
    //             },
    //             {
    //                 "Series", "Intel Core i9"
    //             },
    //             {
    //                 "Model", "12900H"
    //             },
    //             {
    //                 "Quantity of cores", "14"
    //             },
    //             {
    //                 "Quantity of threads", "28"
    //             },
    //             {
    //                 "Base clock", "3.8 Ghz"
    //             },
    //             {
    //                 "Max clock", "5.0 Ghz"
    //             },
    //             {
    //                 "Processor technology", "Intel 7"
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
    //                 "Model family", "ASUS ZenBook"
    //             },
    //             {
    //                 "Operating system", "Windows 11 Professional"
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
    //                 "Manufacturer", "Intel"
    //             },
    //             {
    //                 "Model", "Iris Xe G7 96EU"
    //             },
    //             {
    //                 "Series", "Intel Iris"
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
    //                 "Amount of memory", "1 TB"
    //             }
    //         }
    //     },
    //     {
    //         "Random access memory", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Type", "DDR5"
    //             },
    //             {
    //                 "Amount of memory", "32 GB"
    //             }
    //         }
    //     },
    //     {
    //         "Measurements", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Width", "311 mm"
    //             },
    //             {
    //                 "Length", "221 mm"
    //             },
    //             {
    //                 "Depth", "16 mm"
    //             },
    //             {
    //                 "Weight", "1.5 Kg"
    //             }
    //         }
    //     },
    //     {
    //         "Interfaces and connection", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Network adapters", "Bluetooth, WiFi 802.11ax"
    //             },
    //             {
    //                 "Web-camera", "Present"
    //             },
    //             {
    //                 "Web-camera resolution", "1280x720 1.0 Mp"
    //             },
    //             {
    //                 "Built-in microphone", "Present"
    //             },
    //             {
    //                 "Built-in card reader", "Present"
    //             },
    //             {
    //                 "Supported card types", "MicroSD"
    //             },
    //             {
    //                 "Connectors and I/O ports", "Audio Line out, HDMi, Thunderbolt, USB 3.2"
    //             }
    //         }
    //     },
    //     {
    //         "Display", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Diagonal", "14\""
    //             },
    //             {
    //                 "Resolution", "2880x1800"
    //             },
    //             {
    //                 "Coating", "Glossy"
    //             },
    //             {
    //                 "Matrix type", "OLED"
    //             },
    //             {
    //                 "Display type", "Regular"
    //             },
    //             {
    //                 "Refresh rate", "90 Hz"
    //             }
    //         }
    //     },
    //     {
    //         "Battery", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Type", "Li-ion"
    //             },
    //             {
    //                 "Capacity", "63 Watt-hours"
    //             }
    //         }
    //     },
    //     {
    //         "Additional options", new Dictionary<string, string>()
    //         {
    //             {
    //                 "Optical drive", "Absent"
    //             },
    //             {
    //                 "Numeric keypad", "Present"
    //             },
    //             {
    //                 "Keyboard backlight", "Present"
    //             }
    //         }
    //     },
    // })
    
    internal override IDictionary<string, IEnumerable<string>> Create()
    {
        var template = base.Create();

        template["General"] = GetNewDictionaryValue
            (template, "General", new List<string> { "Model family", "Main color" });
        
        template["Interfaces and connection"] = GetNewDictionaryValue
            (template, "Interfaces and connection", new List<string>
            {
                "Web-camera", "Web-camera resolution", "Built-in microphone",
                "Built-in card reader", "Supported card types"
            });

        AddNewSpecificationToTemplate(template, "Battery", new List<string>
        {
            "Type", "Capacity"
        });
        
        AddNewSpecificationToTemplate(template, "Additional", new List<string>
        {
            "Optical drive", "Numeric keypad", "Keyboard backlight"
        });
        
        AddNewSpecificationToTemplate(template, "Display", new List<string>
        {
            "Diagonal", "Resolution", "Coating",
            "Matrix type", "Display type", "Refresh rate"
        });

        return template;
    }
}