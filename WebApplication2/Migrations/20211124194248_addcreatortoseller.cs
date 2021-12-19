using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addcreatortoseller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productitems_Seller_sellerid",
                table: "productitems");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_Seller_SellerId",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Seller_product_ProductId",
                table: "Seller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seller",
                table: "Seller");

            migrationBuilder.RenameTable(
                name: "Seller",
                newName: "sellers");

            migrationBuilder.RenameIndex(
                name: "IX_Seller_ProductId",
                table: "sellers",
                newName: "IX_sellers_ProductId");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdate",
                table: "sellers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "creatorId",
                table: "sellers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastmodifierId",
                table: "sellers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastmodifydateId",
                table: "sellers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_sellers",
                table: "sellers",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_sellers_creatorId",
                table: "sellers",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_sellers_lastmodifierId",
                table: "sellers",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_sellers_lastmodifydateId",
                table: "sellers",
                column: "lastmodifydateId");

            migrationBuilder.AddForeignKey(
                name: "FK_productitems_sellers_sellerid",
                table: "productitems",
                column: "sellerid",
                principalTable: "sellers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_sellers_SellerId",
                table: "ratings",
                column: "SellerId",
                principalTable: "sellers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sellers_product_ProductId",
                table: "sellers",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sellers_AspNetUsers_creatorId",
                table: "sellers",
                column: "creatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sellers_AspNetUsers_lastmodifierId",
                table: "sellers",
                column: "lastmodifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sellers_AspNetUsers_lastmodifydateId",
                table: "sellers",
                column: "lastmodifydateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productitems_sellers_sellerid",
                table: "productitems");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_sellers_SellerId",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_sellers_product_ProductId",
                table: "sellers");

            migrationBuilder.DropForeignKey(
                name: "FK_sellers_AspNetUsers_creatorId",
                table: "sellers");

            migrationBuilder.DropForeignKey(
                name: "FK_sellers_AspNetUsers_lastmodifierId",
                table: "sellers");

            migrationBuilder.DropForeignKey(
                name: "FK_sellers_AspNetUsers_lastmodifydateId",
                table: "sellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sellers",
                table: "sellers");

            migrationBuilder.DropIndex(
                name: "IX_sellers_creatorId",
                table: "sellers");

            migrationBuilder.DropIndex(
                name: "IX_sellers_lastmodifierId",
                table: "sellers");

            migrationBuilder.DropIndex(
                name: "IX_sellers_lastmodifydateId",
                table: "sellers");

            migrationBuilder.DropColumn(
                name: "createdate",
                table: "sellers");

            migrationBuilder.DropColumn(
                name: "creatorId",
                table: "sellers");

            migrationBuilder.DropColumn(
                name: "lastmodifierId",
                table: "sellers");

            migrationBuilder.DropColumn(
                name: "lastmodifydateId",
                table: "sellers");

            migrationBuilder.RenameTable(
                name: "sellers",
                newName: "Seller");

            migrationBuilder.RenameIndex(
                name: "IX_sellers_ProductId",
                table: "Seller",
                newName: "IX_Seller_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seller",
                table: "Seller",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_productitems_Seller_sellerid",
                table: "productitems",
                column: "sellerid",
                principalTable: "Seller",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_Seller_SellerId",
                table: "ratings",
                column: "SellerId",
                principalTable: "Seller",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_product_ProductId",
                table: "Seller",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
