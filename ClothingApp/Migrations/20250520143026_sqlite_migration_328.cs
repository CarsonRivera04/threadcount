using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingApp.Migrations
{
    /// <inheritdoc />
    public partial class sqlite_migration_328 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClothingTypeSpecific",
                table: "ClothingItem",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ClothingItem",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClothingTypeSpecific",
                table: "ClothingItem");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ClothingItem");
        }
    }
}
