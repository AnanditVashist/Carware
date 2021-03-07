using Microsoft.EntityFrameworkCore.Migrations;

namespace Carware.Migrations
{
    public partial class AddedMileageToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mileage",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "Cars");
        }
    }
}
