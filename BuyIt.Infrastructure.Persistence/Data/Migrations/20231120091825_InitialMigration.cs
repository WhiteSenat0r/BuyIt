using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductManufacturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductManufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(2,1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRatings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecificationAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecificationAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecificationCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecificationCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecificationValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecificationValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecificationCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecificationAttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecificationValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSpecifications_ProductSpecificationAttributes_SpecificationAttributeId",
                        column: x => x.SpecificationAttributeId,
                        principalTable: "ProductSpecificationAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSpecifications_ProductSpecificationCategories_SpecificationCategoryId",
                        column: x => x.SpecificationCategoryId,
                        principalTable: "ProductSpecificationCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSpecifications_ProductSpecificationValues_SpecificationValueId",
                        column: x => x.SpecificationValueId,
                        principalTable: "ProductSpecificationValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    ManufacturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    MainImagesNames = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductManufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "ProductManufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductRatings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "ProductRatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationPairs",
                columns: table => new
                {
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecificationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationPairs", x => new { x.ProductsId, x.SpecificationsId });
                    table.ForeignKey(
                        name: "FK_SpecificationPairs_ProductSpecifications_SpecificationsId",
                        column: x => x.SpecificationsId,
                        principalTable: "ProductSpecifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificationPairs_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_RatingId",
                table: "Products",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecifications_SpecificationAttributeId",
                table: "ProductSpecifications",
                column: "SpecificationAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecifications_SpecificationCategoryId",
                table: "ProductSpecifications",
                column: "SpecificationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecifications_SpecificationValueId",
                table: "ProductSpecifications",
                column: "SpecificationValueId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationPairs_SpecificationsId",
                table: "SpecificationPairs",
                column: "SpecificationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecificationPairs");

            migrationBuilder.DropTable(
                name: "ProductSpecifications");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductSpecificationAttributes");

            migrationBuilder.DropTable(
                name: "ProductSpecificationCategories");

            migrationBuilder.DropTable(
                name: "ProductSpecificationValues");

            migrationBuilder.DropTable(
                name: "ProductManufacturers");

            migrationBuilder.DropTable(
                name: "ProductRatings");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
