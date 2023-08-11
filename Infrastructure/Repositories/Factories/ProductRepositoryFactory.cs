﻿using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.Factories.Common.Classes;

namespace Infrastructure.Repositories.Factories;

public class ProductRepositoryFactory : RepositoryFactory<Product>
{
    public override IRepository<Product> Create(StoreContext dbContext) => 
        new ProductRepository(dbContext);
}