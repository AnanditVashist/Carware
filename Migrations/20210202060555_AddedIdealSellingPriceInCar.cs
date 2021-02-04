using Microsoft.EntityFrameworkCore.Migrations;

namespace Carware.Migrations
{
    public partial class AddedIdealSellingPriceInCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "ActualSellingPrice",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdealSellingPrice",
                table: "Cars",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualSellingPrice",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "IdealSellingPrice",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "SellingPrice",
                table: "Cars",
                type: "int",
                nullable: true);
        }
    }
}
