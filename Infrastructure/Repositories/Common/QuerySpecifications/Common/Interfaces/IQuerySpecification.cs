using System.Linq.Expressions;
using Core.Common.Interfaces;

namespace Infrastructure.Repositories.Common.QuerySpecifications.Common.Interfaces;

public interface IQuerySpecification<TEntity> 
    where TEntity : class, IEntity<Guid>
{
    Expression<Func<TEntity, bool>> Criteria { get; }

    List<Expression<Func<TEntity, object>>> IncludedExpressions { get; } 
    
    Expression<Func<TEntity, object>> OrderByAscendingExpression { get; }
    
    Expression<Func<TEntity, object>> OrderByDescendingExpression { get; }
}