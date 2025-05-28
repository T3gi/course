using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phoenix.Migrations
{
    /// <inheritdoc />
    public partial class BrandTechSpecs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechSpecs",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "TechSpecs",
                table: "Car");
        }
    }
}
