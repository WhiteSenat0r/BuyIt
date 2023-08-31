using System.Linq.Expressions;

namespace Infrastructure.Repositories.Common.QuerySpecifications.Common.Interfaces;

public interface IQuerySpecification<TEntity> 
    where TEntity : class
{
    Expression<Func<TEntity, bool>> Criteria { get; }
    
    Expression<Func<TEntity, bool>> SpecificationCriteria { get; protected init; }

    List<Expression<Func<TEntity, object>>> IncludedExpressions { get; } 
    
    Expression<Func<TEntity, object>> OrderByAscendingExpression { get; }
    
    Expression<Func<TEntity, object>> OrderByDescendingExpression { get; }
    
    int SkippedItemsQuantity { get; }
    
    int TakenItemsQuantity { get; }
    
    bool IsPagingEnabled { get; }
}