using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class initialmigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DType",
                table: "Dish");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Dish",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Dish");

            migrationBuilder.AddColumn<string>(
                name: "DType",
                table: "Dish",
                nullable: false,
                defaultValue: "");
        }
    }
}
