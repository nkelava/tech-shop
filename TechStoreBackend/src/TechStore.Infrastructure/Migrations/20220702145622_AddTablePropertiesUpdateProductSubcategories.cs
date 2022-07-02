using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Infrastructure.Migrations
{
    public partial class AddTablePropertiesUpdateProductSubcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductProperty",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProperty", x => new { x.ProductId, x.PropertyId });
                    table.ForeignKey(
                        name: "FK_ProductProperty_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProperty_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubcategoryProperty",
                columns: table => new
                {
                    SubcategoryId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcategoryProperty", x => new { x.SubcategoryId, x.PropertyId });
                    table.ForeignKey(
                        name: "FK_SubcategoryProperty_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubcategoryProperty_Subcategory_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProperty_PropertyId",
                table: "ProductProperty",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_SubcategoryProperty_PropertyId",
                table: "SubcategoryProperty",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProperty");

            migrationBuilder.DropTable(
                name: "SubcategoryProperty");

            migrationBuilder.DropTable(
                name: "Property");
        }
    }
}
