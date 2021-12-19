using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class ahh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "keypoint",
                columns: table => new
                {
                    title = table.Column<string>(nullable: true),
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    types = table.Column<int>(nullable: false),
                    creatorId = table.Column<string>(nullable: true),
                    creatdatetime = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: false),
                    productId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_keypoint", x => x.id);
                    table.ForeignKey(
                        name: "FK_keypoint_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_keypoint_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_keypoint_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_keypoint_creatorId",
                table: "keypoint",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_keypoint_lastmodifierId",
                table: "keypoint",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_keypoint_productId",
                table: "keypoint",
                column: "productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "keypoint");
        }
    }
}
