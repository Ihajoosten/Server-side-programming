using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddedPropertyToModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordermeals_Order_OrderId",
                table: "Ordermeals");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Ordermeals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MealDate",
                table: "Ordermeals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Ordermeals_Order_OrderId",
                table: "Ordermeals",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordermeals_Order_OrderId",
                table: "Ordermeals");

            migrationBuilder.DropColumn(
                name: "MealDate",
                table: "Ordermeals");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Ordermeals",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Ordermeals_Order_OrderId",
                table: "Ordermeals",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
