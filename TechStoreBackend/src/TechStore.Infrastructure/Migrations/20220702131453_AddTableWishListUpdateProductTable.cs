using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Infrastructure.Migrations
{
    public partial class AddTableWishListUpdateProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WishListProduct",
                columns: table => new
                {
                    WishListId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListProduct", x => new { x.WishListId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WishListProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListProduct_Wishlist_WishListId",
                        column: x => x.WishListId,
                        principalTable: "Wishlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishListProduct_ProductId",
                table: "WishListProduct",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishListProduct");

            migrationBuilder.DropTable(
                name: "Wishlist");
        }
    }
}
