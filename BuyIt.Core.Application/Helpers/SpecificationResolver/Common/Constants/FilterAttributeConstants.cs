namespace Application.Helpers.SpecificationResolver.Common.Constants;

public sealed class FilterAttributeConstants
{
    public IEnumerable<string> RemovedAttributes { get; } = new []
    {
        "Base clock", "Max clock", "Processor technology", 
        "Quantity of threads", "Type of memory", "Memory bus",
        "Drive's interface"
    };
}