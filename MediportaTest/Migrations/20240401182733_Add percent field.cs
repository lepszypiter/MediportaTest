using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediportaTest.Migrations
{
    /// <inheritdoc />
    public partial class Addpercentfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Percent",
                table: "Tags",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percent",
                table: "Tags");
        }
    }
}
