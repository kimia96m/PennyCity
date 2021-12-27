using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addcommenttoproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_comments_productid",
                table: "comments",
                column: "productid");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_product_productid",
                table: "comments",
                column: "productid",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_product_productid",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_productid",
                table: "comments");
        }
    }
}
