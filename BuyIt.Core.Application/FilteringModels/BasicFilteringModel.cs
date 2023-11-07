using Application.Contracts;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;

namespace Application.FilteringModels;

public abstract class BasicFilteringModel : IFilteringModel
{
    private int _itemQuantity = MaximumItemQuantity;

    public const int MaximumItemQuantity = 24;

    public int PageIndex { get; set; } = 1;

    public int ItemQuantity
    {
        get => _itemQuantity;
        set => _itemQuantity = value > MaximumItemQuantity ? MaximumItemQuantity : value;
    }

    protected IDictionary<Type, Type> QuerySpecificationMapping { get; set; } = new Dictionary<Type, Type>();
    
    public List<string> Category { get; set; } = new();

    public List<string> BrandName { get; } = new();

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
}