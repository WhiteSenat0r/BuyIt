﻿using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications;

public class ProductQuerySpecification : BasicProductQuerySpecification
{
    public ProductQuerySpecification() => AddOrderByAscending(p => p.Name);
}