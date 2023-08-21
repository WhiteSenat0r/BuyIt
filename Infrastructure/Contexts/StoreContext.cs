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

    protected override void OnModelCreating(ModelBuilder modelBuilder) // Configuration of database's context
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductType);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Rating);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Manufacturer);

        modelBuilder.Entity<Product>(entity =>
        {
            // Convert IEnumerable<string> to JSON string for storage in the database
            entity.Property(p => p.DescriptionImagesUrls)
                .HasConversion(
                    urls => 
                        JsonSerializer.Serialize(urls, new JsonSerializerOptions()),
                    str => 
                        JsonSerializer.Deserialize
                            <List<string>>(str, new JsonSerializerOptions())!);

            // Convert IDictionary<string, IEnumerable<string>> to JSON string for storage in the database
            entity.Property(p => p.MainImagesUrls)
                .HasConversion(
                    urls => 
                        JsonSerializer.Serialize(urls, new JsonSerializerOptions()),
                    str => 
                        JsonSerializer.Deserialize
                            <List<string>>(str, new JsonSerializerOptions())!);
            
            // Convert IDictionary<string, IDictionary<string, string>>> to JSON string for storage in the database
            entity.Property(p => p.Specifications)
                .HasConversion(
                    urls => 
                        JsonSerializer.Serialize(urls, new JsonSerializerOptions()),
                    str => 
                        JsonSerializer.Deserialize
                            <IDictionary<string, IDictionary<string, string>>>(str, new JsonSerializerOptions())!);
        });
    }
}