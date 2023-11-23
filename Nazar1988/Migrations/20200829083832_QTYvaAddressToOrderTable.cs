using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nazar1988.Migrations
{
    public partial class QTYvaAddressToOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QTY",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QTY",
                table: "Orders");
        }
    }
}
