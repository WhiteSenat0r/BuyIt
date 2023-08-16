using System.Linq.Expressions;
using Core.Common.Interfaces;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Interfaces;

namespace Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

public abstract class QuerySpecification<TEntity> : IQuerySpecification<TEntity>
    where TEntity : class, IEntity<Guid>
{
    protected QuerySpecification() { }
    
    protected QuerySpecification(Expression<Func<TEntity, bool>>? criteria) => Criteria = criteria;

    public Expression<Func<TEntity, bool>>? Criteria { get; }

    public List<Expression<Func<TEntity, object>>> IncludedExpressions { get; } = new();
    
    public Expression<Func<TEntity, object>>? OrderByAscendingExpression { get; private set; }
    
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    protected void AddInclude(Expression<Func<TEntity, object>> includedExpression)
        => IncludedExpressions.Add(includedExpression);
    
    protected void AddIncludeRange
        (IEnumerable<Expression<Func<TEntity, object>>> includedExpressions) =>
        IncludedExpressions.AddRange(includedExpressions);

    protected void AddOrderByAscending(Expression<Func<TEntity, object>> orderByAscendingExpression)
        => OrderByAscendingExpression = orderByAscendingExpression;
    
    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        => OrderByDescendingExpression = orderByDescendingExpression;
}