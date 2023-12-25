namespace Application.Helpers.SpecificationResolver.Common.Constants;

public sealed class FilterCategoryConstants
{
    public IEnumerable<string> RemovedCategories { get; } = new []
    {
        "Measurements", "Interfaces and connection", "Battery",
        "Additional"
    };
}