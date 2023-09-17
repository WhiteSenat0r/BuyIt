﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(StoreContext))]
    partial class StoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Product.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<bool>("InStock")
                        .HasColumnType("bit");

                    b.Property<string>("MainImagesNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ManufacturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<string>("ProductCode")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<Guid>("ProductTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RatingId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("RatingId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Core.Entities.Product.ProductManufacturer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("RegistrationCountry")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("ProductManufacturers");
                });

            modelBuilder.Entity("Core.Entities.Product.ProductRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Score")
                        .HasColumnType("decimal(2, 1)");

                    b.HasKey("Id");

                    b.ToTable("ProductRatings");
                });

            modelBuilder.Entity("Core.Entities.Product.ProductSpecification.ProductSpecification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SpecificationAttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SpecificationCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SpecificationValueId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SpecificationAttributeId");

                    b.HasIndex("SpecificationCategoryId");

                    b.HasIndex("SpecificationValueId");

                    b.ToTable("ProductSpecifications");
                });

            modelBuilder.Entity("Core.Entities.Product.ProductSpecification.ProductSpecificationAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("ProductSpecificationAttributes");
                });

            modelBuilder.Entity("Core.Entities.Product.ProductSpecification.ProductSpecificationCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("ProductSpecificationCategories");
                });

            modelBuilder.Entity("Core.Entities.Product.ProductSpecification.ProductSpecificationValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("ProductSpecificationValues");
                });

            modelBuilder.Entity("Core.Entities.Product.ProductType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("Core.Entities.Product.Product", b =>
                {
                    b.HasOne("Core.Entities.Product.ProductManufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Product.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Product.ProductRating", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");

                    b.Navigation("ProductType");

                    b.Navigation("Rating");
                });

            modelBuilder.Entity("Core.Entities.Product.ProductSpecification.ProductSpecification", b =>
                {
                    b.HasOne("Core.Entities.Product.Product", "Product")
                        .WithMany("Specifications")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Product.ProductSpecification.ProductSpecificationAttribute", "SpecificationAttribute")
                        .WithMany()
                        .HasForeignKey("SpecificationAttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Product.ProductSpecification.ProductSpecificationCategory", "SpecificationCategory")
                        .WithMany()
                        .HasForeignKey("SpecificationCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Product.ProductSpecification.ProductSpecificationValue", "SpecificationValue")
                        .WithMany()
                        .HasForeignKey("SpecificationValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SpecificationAttribute");

                    b.Navigation("SpecificationCategory");

                    b.Navigation("SpecificationValue");
                });

            modelBuilder.Entity("Core.Entities.Product.Product", b =>
                {
                    b.Navigation("Specifications");
                });
#pragma warning restore 612, 618
        }
    }
}
