using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Contracts.RepositoryRelated;

public interface IDatabaseContext
{
    DbSet<Product> Products { get; }
    DbSet<ProductManufacturer> ProductManufacturers { get; }
    DbSet<ProductType> ProductTypes { get; }
    DbSet<ProductRating> ProductRatings { get; }
    DbSet<ProductSpecification> ProductSpecifications { get; }
    DbSet<ProductSpecificationCategory> ProductSpecificationCategories { get; }
    DbSet<ProductSpecificationAttribute> ProductSpecificationAttributes { get; }
    DbSet<ProductSpecificationValue> ProductSpecificationValues { get; }
}