using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingApp.Migrations
{
    /// <inheritdoc />
    public partial class sqlite_migration_172 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ClothingItem",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "ClothingItem");
        }
    }
}
