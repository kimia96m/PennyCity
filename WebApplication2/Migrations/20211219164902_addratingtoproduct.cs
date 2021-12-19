using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addratingtoproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productitems_sellers_sellerid",
                table: "productitems");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ratings",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "sellerid",
                table: "productitems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ratings_ProductId",
                table: "ratings",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_productitems_sellers_sellerid",
                table: "productitems",
                column: "sellerid",
                principalTable: "sellers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_product_ProductId",
                table: "ratings",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productitems_sellers_sellerid",
                table: "productitems");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_product_ProductId",
                table: "ratings");

            migrationBuilder.DropIndex(
                name: "IX_ratings_ProductId",
                table: "ratings");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ratings");

            migrationBuilder.AlterColumn<int>(
                name: "sellerid",
                table: "productitems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_productitems_sellers_sellerid",
                table: "productitems",
                column: "sellerid",
                principalTable: "sellers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
