#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDescriptionImagesProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionImagesNames",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionImagesNames",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
