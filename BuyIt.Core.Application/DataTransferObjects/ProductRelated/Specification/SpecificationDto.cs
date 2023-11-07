namespace Application.DataTransferObjects.ProductRelated.Specification;

public class SpecificationDto
{
    public string Category { get; set; }
    
    public string Attribute { get; set; }
    
    public string Value { get; set; }

    public override string ToString() => $"Category:{Category}|Attribute:{Attribute}|Value:{Value}";
}
