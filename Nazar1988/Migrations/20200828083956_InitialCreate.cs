using Microsoft.EntityFrameworkCore.Migrations;

namespace Nazar1988.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
                );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_UserId",
                table: "Orders");

        }
    }
}
