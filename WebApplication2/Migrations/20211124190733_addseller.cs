using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addseller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sellerid",
                table: "productitems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Seller",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    paid = table.Column<double>(nullable: false),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seller", x => x.id);
                    table.ForeignKey(
                        name: "FK_Seller_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ratings_ProductItemId",
                table: "ratings",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_SellerId",
                table: "ratings",
                column: "SellerId",
                unique: true,
                filter: "[SellerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_productitems_sellerid",
                table: "productitems",
                column: "sellerid");

            migrationBuilder.CreateIndex(
                name: "IX_Seller_ProductId",
                table: "Seller",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_productitems_Seller_sellerid",
                table: "productitems",
                column: "sellerid",
                principalTable: "Seller",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_productitems_ProductItemId",
                table: "ratings",
                column: "ProductItemId",
                principalTable: "productitems",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_Seller_SellerId",
                table: "ratings",
                column: "SellerId",
                principalTable: "Seller",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productitems_Seller_sellerid",
                table: "productitems");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_productitems_ProductItemId",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_Seller_SellerId",
                table: "ratings");

            migrationBuilder.DropTable(
                name: "Seller");

            migrationBuilder.DropIndex(
                name: "IX_ratings_ProductItemId",
                table: "ratings");

            migrationBuilder.DropIndex(
                name: "IX_ratings_SellerId",
                table: "ratings");

            migrationBuilder.DropIndex(
                name: "IX_productitems_sellerid",
                table: "productitems");

            migrationBuilder.DropColumn(
                name: "sellerid",
                table: "productitems");
        }
    }
}
