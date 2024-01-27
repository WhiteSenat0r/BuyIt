using Domain.Contracts.Common;
using Domain.Contracts.RepositoryRelated.Relational;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Specifications.Common;

public static class QuerySpecificationEvaluator 
{
    public static IQueryable<TEntity> GetQuerySpecifications<TEntity>
        (IQueryable<TEntity> innerQueryable, IQuerySpecification<TEntity> querySpecification)
        where TEntity : class, IEntity<Guid>
    {
        var queryable = innerQueryable;

        if (querySpecification.Criteria is not null)
            queryable = queryable.Where(querySpecification.Criteria);
        
        queryable = querySpecification.Includes.Aggregate(queryable, Include);

        if (querySpecification.OrderByAscendingExpression is not null 
            && querySpecification.OrderByDescendingExpression is null)
            queryable = queryable.OrderBy(querySpecification.OrderByAscendingExpression);
        else if (querySpecification.OrderByDescendingExpression is not null 
                 && querySpecification.OrderByAscendingExpression is null)
            queryable = queryable.OrderByDescending(querySpecification.OrderByDescendingExpression);
        
        if (querySpecification.IsPagingEnabled)
            queryable = queryable.Skip(querySpecification.SkippedItemsQuantity)
                .Take(querySpecification.TakenItemsQuantity);
        
        if (querySpecification.IsNotTracked)
            queryable = queryable.AsNoTrackingWithIdentityResolution();
        
        return queryable;
    }
    
    private static IQueryable<TEntity> Include<TEntity>(
        IQueryable<TEntity> queryable, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>> includeExpression) => includeExpression(queryable);
}