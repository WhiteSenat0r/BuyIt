#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedManufacturerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationCountry",
                table: "ProductManufacturers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegistrationCountry",
                table: "ProductManufacturers",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);
        }
    }
}
