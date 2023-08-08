namespace Core.Validators.SpecificationTemplates.Factories.Common;

internal abstract class SpecificationTemplateFactory
{
    internal abstract IDictionary<string, IEnumerable<string>> Create();
}