using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addlastcreatedatetoseller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sellers_AspNetUsers_lastmodifydateId",
                table: "sellers");

            migrationBuilder.DropIndex(
                name: "IX_sellers_lastmodifydateId",
                table: "sellers");

            migrationBuilder.DropColumn(
                name: "lastmodifydateId",
                table: "sellers");

            migrationBuilder.AddColumn<DateTime>(
                name: "lastmodifydate",
                table: "sellers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastmodifydate",
                table: "sellers");

            migrationBuilder.AddColumn<string>(
                name: "lastmodifydateId",
                table: "sellers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sellers_lastmodifydateId",
                table: "sellers",
                column: "lastmodifydateId");

            migrationBuilder.AddForeignKey(
                name: "FK_sellers_AspNetUsers_lastmodifydateId",
                table: "sellers",
                column: "lastmodifydateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
