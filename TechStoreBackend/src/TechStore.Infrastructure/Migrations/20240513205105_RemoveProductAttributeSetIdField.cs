using Microsoft.EntityFrameworkCore.Migrations;


namespace TechStore.Infrastructure.Migrations
{
    public partial class RemoveProductAttributeSetIdField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductAttributeSet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductAttributeSet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
