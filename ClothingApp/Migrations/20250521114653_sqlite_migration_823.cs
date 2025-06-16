using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingApp.Migrations
{
    /// <inheritdoc />
    public partial class sqlite_migration_823 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ClothingItem",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "ClothingItem");
        }
    }
}
