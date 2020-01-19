using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangedModelMealDish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderMealDishes_Ordermeals_OrderMealId",
                table: "OrderMealDishes");

            migrationBuilder.AlterColumn<int>(
                name: "OrderMealId",
                table: "OrderMealDishes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMealDishes_Ordermeals_OrderMealId",
                table: "OrderMealDishes",
                column: "OrderMealId",
                principalTable: "Ordermeals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderMealDishes_Ordermeals_OrderMealId",
                table: "OrderMealDishes");

            migrationBuilder.AlterColumn<int>(
                name: "OrderMealId",
                table: "OrderMealDishes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMealDishes_Ordermeals_OrderMealId",
                table: "OrderMealDishes",
                column: "OrderMealId",
                principalTable: "Ordermeals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
