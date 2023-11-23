using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nazar1988.Migrations
{
    public partial class changeDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Discounts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndtDate",
                table: "Discounts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndtDate",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
