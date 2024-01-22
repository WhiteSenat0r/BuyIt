﻿using Domain.Entities.ProductRelated;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.Common.Classes;
using Persistence.Repositories.ProductRelatedRepositories;

namespace Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;

public class ProductTypeRepositoryFactory : RepositoryFactory<ProductType>
{
    public override ProductTypeRepository Create(StoreContext dbContext) =>  new(dbContext);
}