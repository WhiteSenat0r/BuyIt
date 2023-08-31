using System.Linq.Expressions;
using Core.Common.Interfaces;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Interfaces;

namespace Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

public abstract class QuerySpecification<TEntity> : IQuerySpecification<TEntity>
    where TEntity : class, IEntity<Guid>
{
    protected QuerySpecification() { }
    
    protected QuerySpecification(Expression<Func<TEntity, bool>> mainCriteria) => Criteria = mainCriteria;

    public Expression<Func<TEntity, bool>> Criteria { get; init; }
    
    public Expression<Func<TEntity, bool>> SpecificationCriteria { get; init; }

    public List<Expression<Func<TEntity, object>>> IncludedExpressions { get; } = new();
    
    public Expression<Func<TEntity, object>> OrderByAscendingExpression { get; private set; }
    
    public Expression<Func<TEntity, object>> OrderByDescendingExpression { get; private set; }
    
    public int SkippedItemsQuantity { get; private set; }
    
    public int TakenItemsQuantity { get; private set; }
    
    public bool IsPagingEnabled { get; private set; }

    protected void AddInclude(Expression<Func<TEntity, object>> includedExpression)
        => IncludedExpressions.Add(includedExpression);
    
    protected void AddIncludeRange
        (IEnumerable<Expression<Func<TEntity, object>>> includedExpressions) =>
        IncludedExpressions.AddRange(includedExpressions);

    protected void AddPaging(int takenItemsQuantity, int skippedItemsQuantity)
    {
        TakenItemsQuantity = takenItemsQuantity;
        SkippedItemsQuantity = skippedItemsQuantity;
        IsPagingEnabled = true;
    }

    protected void AddOrderByAscending(Expression<Func<TEntity, object>> orderByAscendingExpression)
        => OrderByAscendingExpression = orderByAscendingExpression;
    
    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        => OrderByDescendingExpression = orderByDescendingExpression;
}