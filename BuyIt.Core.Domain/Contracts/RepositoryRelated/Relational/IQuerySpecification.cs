using System.Linq.Expressions;

namespace Domain.Contracts.RepositoryRelated.Relational;

public interface IQuerySpecification<TEntity> 
    where TEntity : class
{
    Expression<Func<TEntity, bool>> Criteria { get; }

    IEnumerable<string> IncludeStrings { get; }
    
    Expression<Func<TEntity, object>> OrderByAscendingExpression { get; }
    
    Expression<Func<TEntity, object>> OrderByDescendingExpression { get; }
    
    int SkippedItemsQuantity { get; }
    
    int TakenItemsQuantity { get; }
    
    bool IsPagingEnabled { get; set; }
    
    public bool IsNotTracked { get; set; }
}