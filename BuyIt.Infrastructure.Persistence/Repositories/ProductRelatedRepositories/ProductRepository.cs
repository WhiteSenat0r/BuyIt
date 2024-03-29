﻿using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.ProductRelatedRepositories;

public sealed class ProductRepository : GenericRepository<Product>
{
    internal ProductRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}