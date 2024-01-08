using Application.Contracts;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities;

namespace Application.FilteringModels;

public abstract class BasicFilteringModel : IFilteringModel
{
    private int _itemQuantity = MaximumItemQuantity;

    public const int MaximumItemQuantity = 24;

    protected BasicFilteringModel() => MappedFilterNamings = GetMappedFilterNamings();

    public int PageIndex { get; set; } = 1;

    public int ItemQuantity
    {
        get => _itemQuantity;
        set => _itemQuantity = value > MaximumItemQuantity ? MaximumItemQuantity : value;
    }

    public IDictionary<string, IDictionary<string, string>> MappedFilterNamings { get; set; }

    protected IDictionary<Type, Type> QuerySpecificationMapping { get; set; } = new Dictionary<Type, Type>();
    
    public List<string> Category { get; set; } = new();

    public List<string> BrandName { get; set; } = new();

    public decimal? LowerPriceLimit { get; set; }
    
    public decimal? UpperPriceLimit { get; set; }
    
    public string InStock { get; set; }

    public string SortingType { get; set; }

    public IQuerySpecification<Product> CreateQuerySpecification()
    {
        var specificationType = QuerySpecificationMapping[GetType()];
        var specification = Activator.CreateInstance(specificationType, this);
        return specification as IQuerySpecification<Product>;
    }

    protected virtual IDictionary<string, IDictionary<string, string>> GetMappedFilterNamings() => 
        new Dictionary<string, IDictionary<string, string>>();
}