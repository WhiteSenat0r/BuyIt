using Core.Common.Interfaces;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

public static class QuerySpecificationEvaluator 
{
    public static IQueryable<TEntity> GetQuerySpecifications<TEntity>
        (IQueryable<TEntity> innerQueryable, IQuerySpecification<TEntity> querySpecification)
        where TEntity : class, IEntity<Guid>
    {
        var queryable = innerQueryable;

        if (querySpecification.Criteria is not null)
            queryable = queryable.Where(querySpecification.Criteria);

        queryable = querySpecification.IncludedExpressions.Aggregate
            (queryable, (current, includeExpression) =>
                current.Include(includeExpression));
        
        if (querySpecification.OrderByAscendingExpression is not null 
            && querySpecification.OrderByDescendingExpression is null)
            queryable = queryable.OrderBy(querySpecification.OrderByAscendingExpression);
        else if (querySpecification.OrderByDescendingExpression is not null 
                 && querySpecification.OrderByAscendingExpression is null)
            queryable = queryable.OrderByDescending(querySpecification.OrderByDescendingExpression);

        return queryable;
    }
}