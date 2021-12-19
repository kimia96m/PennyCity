using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class add_tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    state = table.Column<byte>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_tags_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tags_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tagvalues",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    state = table.Column<byte>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true),
                    tagsid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tagvalues", x => x.id);
                    table.ForeignKey(
                        name: "FK_tagvalues_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tagvalues_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tagvalues_tags_tagsid",
                        column: x => x.tagsid,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tags_creatorId",
                table: "tags",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_tags_lastmodifierId",
                table: "tags",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_tagvalues_creatorId",
                table: "tagvalues",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_tagvalues_lastmodifierId",
                table: "tagvalues",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_tagvalues_tagsid",
                table: "tagvalues",
                column: "tagsid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tagvalues");

            migrationBuilder.DropTable(
                name: "tags");
        }
    }
}
