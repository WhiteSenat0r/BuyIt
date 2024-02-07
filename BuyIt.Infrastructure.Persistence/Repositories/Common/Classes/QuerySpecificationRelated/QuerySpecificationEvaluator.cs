using Domain.Contracts.Common;
using Domain.Contracts.RepositoryRelated.Relational;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Common.Classes.QuerySpecificationRelated;

public static class QuerySpecificationEvaluator 
{
    public static IQueryable<TEntity> GetQuerySpecifications<TEntity>
        (IQueryable<TEntity> innerQueryable, IQuerySpecification<TEntity> querySpecification)
        where TEntity : class, IEntity<Guid>
    {
        var queryable = innerQueryable;

        if (querySpecification.Criteria is not null)
            queryable = queryable.Where(querySpecification.Criteria);
        
        queryable = querySpecification.IncludeStrings.Aggregate(queryable,
            (currentQueryElement, includedProperty) =>
                currentQueryElement.Include(includedProperty));

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
}