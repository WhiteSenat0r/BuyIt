﻿using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.ProductRelatedRepositories;

public sealed class ProductSpecificationValueRepository : GenericRepository<ProductSpecificationValue>
{
    internal ProductSpecificationValueRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}