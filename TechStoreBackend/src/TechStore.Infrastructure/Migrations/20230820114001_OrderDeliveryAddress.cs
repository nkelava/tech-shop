using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Infrastructure.Migrations
{
    public partial class OrderDeliveryAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliveryAddress_DeliveryAddressId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_DeliveryAddressId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DeliveryAddressId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "DeliveryAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddress_OrderId",
                table: "DeliveryAddress",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAddress_Order_OrderId",
                table: "DeliveryAddress",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAddress_Order_OrderId",
                table: "DeliveryAddress");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryAddress_OrderId",
                table: "DeliveryAddress");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "DeliveryAddress");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryAddressId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeliveryAddressId",
                table: "Order",
                column: "DeliveryAddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliveryAddress_DeliveryAddressId",
                table: "Order",
                column: "DeliveryAddressId",
                principalTable: "DeliveryAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
