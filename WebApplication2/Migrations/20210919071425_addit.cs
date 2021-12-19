using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_Brand_Brandsid",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_group_Groupsid",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_Brandsid",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_Groupsid",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Brandsid",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Groupsid",
                table: "product");

            migrationBuilder.AlterColumn<byte>(
                name: "state",
                table: "product",
                nullable: true,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<int>(
                name: "Brandid",
                table: "product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Groupid",
                table: "product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdatetime",
                table: "group",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "State",
                table: "group",
                nullable: true,
                oldClrType: typeof(byte));

            migrationBuilder.CreateIndex(
                name: "IX_product_Brandid",
                table: "product",
                column: "Brandid");

            migrationBuilder.CreateIndex(
                name: "IX_product_Groupid",
                table: "product",
                column: "Groupid");

            migrationBuilder.AddForeignKey(
                name: "FK_product_Brand_Brandid",
                table: "product",
                column: "Brandid",
                principalTable: "Brand",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_group_Groupid",
                table: "product",
                column: "Groupid",
                principalTable: "group",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_Brand_Brandid",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_group_Groupid",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_Brandid",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_Groupid",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Brandid",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Groupid",
                table: "product");

            migrationBuilder.AlterColumn<byte>(
                name: "state",
                table: "product",
                nullable: false,
                oldClrType: typeof(byte),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Brandsid",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Groupsid",
                table: "product",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdatetime",
                table: "group",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<byte>(
                name: "State",
                table: "group",
                nullable: false,
                oldClrType: typeof(byte),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_Brandsid",
                table: "product",
                column: "Brandsid");

            migrationBuilder.CreateIndex(
                name: "IX_product_Groupsid",
                table: "product",
                column: "Groupsid");

            migrationBuilder.AddForeignKey(
                name: "FK_product_Brand_Brandsid",
                table: "product",
                column: "Brandsid",
                principalTable: "Brand",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_group_Groupsid",
                table: "product",
                column: "Groupsid",
                principalTable: "group",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
