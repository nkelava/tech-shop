using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Infrastructure.Migrations
{
    public partial class AddUsernameColumnToCartTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Cart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Cart");
        }
    }
}
