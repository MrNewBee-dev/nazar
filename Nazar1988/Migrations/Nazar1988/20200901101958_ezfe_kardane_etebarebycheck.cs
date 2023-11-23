using Microsoft.EntityFrameworkCore.Migrations;

namespace Nazar1988.Migrations.Nazar1988
{
    public partial class ezfe_kardane_etebarebycheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Shomare_Check",
                table: "wallets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shomare_Check",
                table: "wallets");
        }
    }
}
