using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Domain.Contracts.RepositoryRelated.Relational;

public interface IQuerySpecification<TEntity> 
    where TEntity : class
{
    Expression<Func<TEntity, bool>> Criteria { get; }

    List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; }
    
    Expression<Func<TEntity, object>> OrderByAscendingExpression { get; }
    
    Expression<Func<TEntity, object>> OrderByDescendingExpression { get; }
    
    int SkippedItemsQuantity { get; }
    
    int TakenItemsQuantity { get; }
    
    bool IsPagingEnabled { get; set; }
    
    public bool IsNotTracked { get; set; }
}