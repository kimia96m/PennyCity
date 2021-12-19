using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class bannergroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bannergroup",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    groupnumber = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdatetime = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bannergroup", x => x.id);
                    table.ForeignKey(
                        name: "FK_bannergroup_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bannergroup_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bannergroup_creatorId",
                table: "bannergroup",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_bannergroup_lastmodifierId",
                table: "bannergroup",
                column: "lastmodifierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bannergroup");
        }
    }
}
