using Domain.Contracts.RepositoryRelated;
using Domain.Entities;

namespace Application.Contracts;

public interface IFilteringModel
{
    List<string> Category { get; }
    
    List<string> BrandName { get; }

    decimal? LowerPriceLimit { get; }
    
    decimal? UpperPriceLimit { get; }
    
    string InStock { get; } // true - in stock, false - not in stock
    
    string SortingType { get; }
    
    int PageIndex { get; }

    int ItemQuantity { get; }
    
    IDictionary<string, IDictionary<string,string>> MappedFilterNamings { get; }

    IQuerySpecification<Product> CreateQuerySpecification();
}