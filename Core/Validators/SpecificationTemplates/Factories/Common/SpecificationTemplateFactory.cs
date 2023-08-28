namespace Core.Validators.SpecificationTemplates.Factories.Common;

internal abstract class SpecificationTemplateFactory
{
    internal abstract IDictionary<string, IEnumerable<string>> Create();
    
    protected static IEnumerable<string> GetNewDictionaryValue(
        IDictionary<string, IEnumerable<string>> dictionary,
        string key, IEnumerable<string> addedItems)
    {
        dictionary.TryGetValue(key, out var collection);

        var extractedCollection = collection!.ToList();
        
        extractedCollection.AddRange(addedItems);

        return extractedCollection;
    }

    protected static void AddNewSpecificationToTemplate(IDictionary<string, IEnumerable<string>> dictionary,
        string newKey, IEnumerable<string> newValue) => dictionary.Add
            (new KeyValuePair<string, IEnumerable<string>>(newKey, newValue));
}