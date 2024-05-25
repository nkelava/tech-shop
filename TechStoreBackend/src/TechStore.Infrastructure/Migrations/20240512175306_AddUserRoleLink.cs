using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Infrastructure.Migrations
{
    public partial class AddUserRoleLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListProduct_Product_ProductId",
                table: "WishListProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_WishListProduct_Wishlist_WishListId",
                table: "WishListProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishListProduct",
                table: "WishListProduct");

            migrationBuilder.RenameTable(
                name: "WishListProduct",
                newName: "WishlistProduct");

            migrationBuilder.RenameColumn(
                name: "WishListId",
                table: "WishlistProduct",
                newName: "WishlistId");

            migrationBuilder.RenameIndex(
                name: "IX_WishListProduct_ProductId",
                table: "WishlistProduct",
                newName: "IX_WishlistProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishlistProduct",
                table: "WishlistProduct",
                columns: new[] { "WishlistId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistProduct_Product_ProductId",
                table: "WishlistProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistProduct_Wishlist_WishlistId",
                table: "WishlistProduct",
                column: "WishlistId",
                principalTable: "Wishlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistProduct_Product_ProductId",
                table: "WishlistProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistProduct_Wishlist_WishlistId",
                table: "WishlistProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistProduct",
                table: "WishlistProduct");

            migrationBuilder.RenameTable(
                name: "WishlistProduct",
                newName: "WishListProduct");

            migrationBuilder.RenameColumn(
                name: "WishlistId",
                table: "WishListProduct",
                newName: "WishListId");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistProduct_ProductId",
                table: "WishListProduct",
                newName: "IX_WishListProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishListProduct",
                table: "WishListProduct",
                columns: new[] { "WishListId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WishListProduct_Product_ProductId",
                table: "WishListProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishListProduct_Wishlist_WishListId",
                table: "WishListProduct",
                column: "WishListId",
                principalTable: "Wishlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
