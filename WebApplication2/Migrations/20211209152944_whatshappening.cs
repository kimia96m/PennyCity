using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class whatshappening : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sellerid",
                table: "productitems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "bannergroups",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    groupnumber = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdatetime = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true),
                    ext = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bannergroups", x => x.id);
                    table.ForeignKey(
                        name: "FK_bannergroups_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bannergroups_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    text = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    userId = table.Column<string>(nullable: true),
                    productid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_comments_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sellers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    paid = table.Column<double>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sellers", x => x.id);
                    table.ForeignKey(
                        name: "FK_sellers_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sellers_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductItemId = table.Column<int>(nullable: true),
                    SellerId = table.Column<int>(nullable: true),
                    Rate = table.Column<int>(nullable: false),
                    customer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ratings_sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "sellers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productitems_sellerid",
                table: "productitems",
                column: "sellerid");

            migrationBuilder.CreateIndex(
                name: "IX_bannergroups_creatorId",
                table: "bannergroups",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_bannergroups_lastmodifierId",
                table: "bannergroups",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_userId",
                table: "comments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_SellerId",
                table: "ratings",
                column: "SellerId",
                unique: true,
                filter: "[SellerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_sellers_creatorId",
                table: "sellers",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_sellers_lastmodifierId",
                table: "sellers",
                column: "lastmodifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_productitems_sellers_sellerid",
                table: "productitems",
                column: "sellerid",
                principalTable: "sellers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productitems_sellers_sellerid",
                table: "productitems");

            migrationBuilder.DropTable(
                name: "bannergroups");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.DropTable(
                name: "sellers");

            migrationBuilder.DropIndex(
                name: "IX_productitems_sellerid",
                table: "productitems");

            migrationBuilder.DropColumn(
                name: "sellerid",
                table: "productitems");
        }
    }
}
