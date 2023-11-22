using System.Text.Json;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public sealed class StoreContext : DbContext, IDatabaseContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<ProductManufacturer> ProductManufacturers { get; set; } = null!;

    public DbSet<ProductType> ProductTypes { get; set; } = null!;

    public DbSet<ProductRating> ProductRatings { get; set; } = null!;
    
    public DbSet<ProductSpecification> ProductSpecifications { get; set; } = null!;
    
    public DbSet<ProductSpecificationCategory> ProductSpecificationCategories { get; set; } = null!;
    
    public DbSet<ProductSpecificationAttribute> ProductSpecificationAttributes { get; set; } = null!;
    
    public DbSet<ProductSpecificationValue> ProductSpecificationValues { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) // Configuration of database's context
    {
        SpecifyEntityRelations(modelBuilder);
        MapEntities(modelBuilder);
    }

    private void MapEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            // Convert IDictionary<string, IEnumerable<string>> to JSON string for storage in the database
            entity.Property(p => p.MainImagesNames)
                .HasConversion(
                    urls =>
                        JsonSerializer.Serialize(urls, new JsonSerializerOptions()),
                    str =>
                        JsonSerializer.Deserialize
                            <List<string>>(str, new JsonSerializerOptions())!);
        });
    }

    private void SpecifyEntityRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductType);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Rating);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Manufacturer);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Specifications)
            .WithMany(s => s.Products)
            .UsingEntity(e => e.ToTable("SpecificationPairs"));

        modelBuilder.Entity<ProductSpecification>()
            .HasKey(p => p.Id);
        
        modelBuilder.Entity<ProductSpecification>()
            .HasOne(c => c.SpecificationCategory);
        
        modelBuilder.Entity<ProductSpecification>()
            .HasOne(a => a.SpecificationAttribute);
        
        modelBuilder.Entity<ProductSpecification>()
            .HasOne(v => v.SpecificationValue);

        modelBuilder.Entity<ProductSpecificationAttribute>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<ProductSpecificationCategory>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<ProductSpecificationValue>()
            .HasKey(p => p.Id);
    }
}