using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncreasedSpecificationValueLenght : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ProductSpecifications",
                type: "nvarchar(192)",
                maxLength: 192,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(96)",
                oldMaxLength: 96,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ProductSpecifications",
                type: "nvarchar(96)",
                maxLength: 96,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(192)",
                oldMaxLength: 192,
                oldNullable: true);
        }
    }
}
