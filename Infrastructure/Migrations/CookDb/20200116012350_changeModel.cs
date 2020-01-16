using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.CookDb
{
    public partial class changeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "Dish",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dish_MealId",
                table: "Dish",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dish_Meal_MealId",
                table: "Dish",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dish_Meal_MealId",
                table: "Dish");

            migrationBuilder.DropIndex(
                name: "IX_Dish_MealId",
                table: "Dish");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "Dish");
        }
    }
}
