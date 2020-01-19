using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordermeals_Meal_MealId",
                table: "Ordermeals");

            migrationBuilder.DropIndex(
                name: "IX_Ordermeals_MealId",
                table: "Ordermeals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ordermeals_MealId",
                table: "Ordermeals",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordermeals_Meal_MealId",
                table: "Ordermeals",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
