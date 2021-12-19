using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class orderstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fishnumber",
                table: "orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "registrationdate",
                table: "orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "state",
                table: "orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fishnumber",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "registrationdate",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "state",
                table: "orders");
        }
    }
}
