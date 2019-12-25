using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addedmenuMeals2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Menu_MenuId",
                table: "Meal");

            migrationBuilder.DropIndex(
                name: "IX_Meal_MenuId",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Meal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "Meal",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meal_MenuId",
                table: "Meal",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Menu_MenuId",
                table: "Meal",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
