using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities;

namespace Application.Contracts;

public interface IFilteringModel
{
    List<string> Category { get; set; }
    
    List<string> BrandName { get; set; }

    decimal? LowerPriceLimit { get; set; }
    
    decimal? UpperPriceLimit { get; set; }
    
    string InStock { get; set; } // true - in stock, false - not in stock
    
    string SortingType { get; set; }
    
    int PageIndex { get; set; }

    int ItemQuantity { get; set; }
    
    IDictionary<string, IDictionary<string,string>> MappedFilterNamings { get; set; }

    IQuerySpecification<Product> CreateQuerySpecification();
}