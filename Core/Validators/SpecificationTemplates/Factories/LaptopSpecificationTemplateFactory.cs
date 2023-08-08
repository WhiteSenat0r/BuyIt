namespace Core.Validators.SpecificationTemplates.Factories;

internal class LaptopSpecificationTemplateFactory : ComputerSpecificationTemplateFactory
{
    internal override IDictionary<string, IEnumerable<string>> Create()
    {
        var template = base.Create();

        template["General"] = GetNewDictionaryValue
            (template, "General", new List<string>(){ "Model family" });
        
        template["Interfaces and connection"] = GetNewDictionaryValue
            (template, "Interfaces and connection", new List<string>()
            {
                "Web-camera", "Web-camera resolution", "Built-in microphone",
                "Built-in card reader", "Supported card types"
            });

        AddNewSpecificationToTemplate(template, "Battery", new List<string>()
        {
            "Type", "Capacity"
        });
        
        AddNewSpecificationToTemplate(template, "Additional options", new List<string>()
        {
            "Optical drive", "Numeric keypad", "Keyboard backlight"
        });
        
        AddNewSpecificationToTemplate(template, "Display", new List<string>()
        {
            "Diagonal", "Resolution", "Coating",
            "Matrix type", "Display type", "Refresh rate"
        });

        return template;
    }
    
    private static IEnumerable<string> GetNewDictionaryValue(
        IDictionary<string, IEnumerable<string>> dictionary,
        string key, IEnumerable<string> addedItems)
    {
        dictionary.TryGetValue(key, out var collection);

        var extractedCollection = collection!.ToList();
        
        extractedCollection.AddRange(addedItems);

        return extractedCollection;
    }

    private static void AddNewSpecificationToTemplate(
        IDictionary<string, IEnumerable<string>> dictionary,
        string newKey, IEnumerable<string> newValue)
    {
        dictionary.Add
            (new KeyValuePair<string, IEnumerable<string>>(newKey, newValue));
    }
}