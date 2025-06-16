using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingApp.Migrations
{
    /// <inheritdoc />
    public partial class sqlite_migration_144 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClothingItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClothingType = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Condition = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    PurchaseDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    LastWorn = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    NumberWorn = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingItem", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothingItem");
        }
    }
}
