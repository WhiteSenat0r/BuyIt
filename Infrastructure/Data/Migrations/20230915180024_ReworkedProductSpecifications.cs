using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReworkedProductSpecifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attribute",
                table: "ProductSpecifications");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "ProductSpecifications");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ProductSpecifications");

            migrationBuilder.AddColumn<Guid>(
                name: "SpecificationAttributeId",
                table: "ProductSpecifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SpecificationCategoryId",
                table: "ProductSpecifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SpecificationValueId",
                table: "ProductSpecifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecifications_ProductSpecificationAttributes_SpecificationAttributeId",
                table: "ProductSpecifications",
                column: "SpecificationAttributeId",
                principalTable: "ProductSpecificationAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecifications_ProductSpecificationCategories_SpecificationCategoryId",
                table: "ProductSpecifications",
                column: "SpecificationCategoryId",
                principalTable: "ProductSpecificationCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecifications_ProductSpecificationValues_SpecificationValueId",
                table: "ProductSpecifications",
                column: "SpecificationValueId",
                principalTable: "ProductSpecificationValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecifications_ProductSpecificationAttributes_SpecificationAttributeId",
                table: "ProductSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecifications_ProductSpecificationCategories_SpecificationCategoryId",
                table: "ProductSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecifications_ProductSpecificationValues_SpecificationValueId",
                table: "ProductSpecifications");

            migrationBuilder.DropTable(
                name: "ProductSpecificationAttributes");

            migrationBuilder.DropTable(
                name: "ProductSpecificationCategories");

            migrationBuilder.DropTable(
                name: "ProductSpecificationValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecifications_SpecificationAttributeId",
                table: "ProductSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecifications_SpecificationCategoryId",
                table: "ProductSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecifications_SpecificationValueId",
                table: "ProductSpecifications");

            migrationBuilder.DropColumn(
                name: "SpecificationAttributeId",
                table: "ProductSpecifications");

            migrationBuilder.DropColumn(
                name: "SpecificationCategoryId",
                table: "ProductSpecifications");

            migrationBuilder.DropColumn(
                name: "SpecificationValueId",
                table: "ProductSpecifications");

            migrationBuilder.AddColumn<string>(
                name: "Attribute",
                table: "ProductSpecifications",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ProductSpecifications",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "ProductSpecifications",
                type: "nvarchar(192)",
                maxLength: 192,
                nullable: true);
        }
    }
}
