using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Infrastructure.Migrations
{
    public partial class UpdateRefreshTokenFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isUsed",
                table: "RefreshToken",
                newName: "IsUsed");

            migrationBuilder.RenameColumn(
                name: "isRevoked",
                table: "RefreshToken",
                newName: "IsRevoked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "RefreshToken",
                newName: "isUsed");

            migrationBuilder.RenameColumn(
                name: "IsRevoked",
                table: "RefreshToken",
                newName: "isRevoked");
        }
    }
}
