using Application.Specifications;

namespace Application.FilteringModels;

public sealed class ProductSearchFilteringModel : BasicFilteringModel
{
    public ProductSearchFilteringModel() => QuerySpecificationMapping
        [typeof(ProductSearchFilteringModel)] = typeof(ProductSearchQuerySpecification);
    
    public string Text { get; set; } = null!;
}