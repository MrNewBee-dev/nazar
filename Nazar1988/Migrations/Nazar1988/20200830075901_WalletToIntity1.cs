using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nazar1988.Migrations.Nazar1988
{
    public partial class WalletToIntity1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "walleTTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false),
                    TypeTitle = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_walleTTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "wallets",
                columns: table => new
                {
                    WalletId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    IsPay = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallets", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_wallets_walleTTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "walleTTypes",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wallets_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wallets_TypeId",
                table: "wallets",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_wallets_UserId",
                table: "wallets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wallets");

            migrationBuilder.DropTable(
                name: "walleTTypes");
        }
    }
}
