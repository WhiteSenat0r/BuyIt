﻿using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Common.Classes;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelatedRepositories;

public sealed class ProductTypeRepository : GenericRepository<ProductType>
{
    internal ProductTypeRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}