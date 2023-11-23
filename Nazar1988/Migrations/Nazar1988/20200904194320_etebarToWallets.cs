using Microsoft.EntityFrameworkCore.Migrations;

namespace Nazar1988.Migrations.Nazar1988
{
    public partial class etebarToWallets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Etebar",
                table: "wallets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Etebar",
                table: "wallets");
        }
    }
}
