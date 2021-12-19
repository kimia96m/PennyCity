using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class additems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productitems",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    productId = table.Column<int>(nullable: true),
                    tagvaluesid = table.Column<int>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    discount = table.Column<double>(nullable: true),
                    quantity = table.Column<byte>(nullable: false),
                    state = table.Column<byte>(nullable: false),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productitems", x => x.id);
                    table.ForeignKey(
                        name: "FK_productitems_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productitems_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productitems_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productitems_tagvalues_tagvaluesid",
                        column: x => x.tagvaluesid,
                        principalTable: "tagvalues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productitems_creatorId",
                table: "productitems",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_productitems_lastmodifierId",
                table: "productitems",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_productitems_productId",
                table: "productitems",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_productitems_tagvaluesid",
                table: "productitems",
                column: "tagvaluesid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productitems");
        }
    }
}
