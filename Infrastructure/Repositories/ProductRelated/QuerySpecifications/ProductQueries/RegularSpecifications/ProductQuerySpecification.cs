using System.Linq.Expressions;
using Core.Entities.Product;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications;

public sealed class ProductQuerySpecification : BasicProductQuerySpecification
{
    public ProductQuerySpecification() => AddOrderByAscending(p => p.Name);

    public ProductQuerySpecification(Expression<Func<Product, bool>> expression) : base(expression) 
        => AddOrderByAscending(p => p.Name);
}