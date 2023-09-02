using System.Text.Json;
using Core.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<ProductManufacturer> ProductManufacturers { get; set; } = null!;

    public DbSet<ProductType> ProductTypes { get; set; } = null!;

    public DbSet<ProductRating> ProductRatings { get; set; } = null!;
    
    public DbSet<ProductSpecification> ProductSpecifications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) // Configuration of database's context
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductType);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Rating);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Manufacturer);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Specifications);
        
        modelBuilder.Entity<ProductSpecification>()
            .HasKey(p => p.Id);

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
}