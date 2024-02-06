using System.Linq.Expressions;
using Domain.Contracts.Common;
using Domain.Contracts.RepositoryRelated.Relational;

namespace Application.Specifications.Common;

public abstract class QuerySpecification<TEntity> : IQuerySpecification<TEntity>
    where TEntity : class, IEntity<Guid>
{
    protected QuerySpecification() { }
    
    protected QuerySpecification(Expression<Func<TEntity, bool>> mainCriteria) => Criteria = mainCriteria;

    public Expression<Func<TEntity, bool>> Criteria { get; protected init; }
    
    public IEnumerable<string> IncludeStrings { get; } = new List<string>();
    
    public Expression<Func<TEntity, object>> OrderByAscendingExpression { get; private set; }
    
    public Expression<Func<TEntity, object>> OrderByDescendingExpression { get; private set; }
    
    public int SkippedItemsQuantity { get; private set; }
    
    public int TakenItemsQuantity { get; private set; }
    
    public bool IsPagingEnabled { get; set; }
    
    public bool IsNotTracked { get; set; }
    
    protected void AddPaging(int takenItemsQuantity, int skippedItemsQuantity)
    {
        TakenItemsQuantity = takenItemsQuantity;
        SkippedItemsQuantity = skippedItemsQuantity;
        IsPagingEnabled = true;
    }
    
    protected void AddInclude(string includeString) 
        => (IncludeStrings as List<string>)!.Add(includeString);

    protected void AddOrderByAscending(Expression<Func<TEntity, object>> orderByAscendingExpression)
        => OrderByAscendingExpression = orderByAscendingExpression;
    
    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        => OrderByDescendingExpression = orderByDescendingExpression;
}