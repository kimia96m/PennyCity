using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addspe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "specificationgroups",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    groupid = table.Column<int>(nullable: true),
                    state = table.Column<byte>(nullable: false),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specificationgroups", x => x.id);
                    table.ForeignKey(
                        name: "FK_specificationgroups_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specificationgroups_group_groupid",
                        column: x => x.groupid,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specificationgroups_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "specifications",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    state = table.Column<byte>(nullable: false),
                    specificationgroupsid = table.Column<int>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_specifications_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specifications_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specifications_specificationgroups_specificationgroupsid",
                        column: x => x.specificationgroupsid,
                        principalTable: "specificationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "specificationvalues",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    valuetitle = table.Column<string>(nullable: true),
                    state = table.Column<byte>(nullable: false),
                    productId = table.Column<int>(nullable: true),
                    specificationid = table.Column<int>(nullable: true),
                    creatorId = table.Column<string>(nullable: true),
                    createdate = table.Column<DateTime>(nullable: false),
                    lastmodifierId = table.Column<string>(nullable: true),
                    lastmodifydate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specificationvalues", x => x.id);
                    table.ForeignKey(
                        name: "FK_specificationvalues_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specificationvalues_AspNetUsers_lastmodifierId",
                        column: x => x.lastmodifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specificationvalues_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specificationvalues_specifications_specificationid",
                        column: x => x.specificationid,
                        principalTable: "specifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_specificationgroups_creatorId",
                table: "specificationgroups",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_specificationgroups_groupid",
                table: "specificationgroups",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "IX_specificationgroups_lastmodifierId",
                table: "specificationgroups",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_specifications_creatorId",
                table: "specifications",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_specifications_lastmodifierId",
                table: "specifications",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_specifications_specificationgroupsid",
                table: "specifications",
                column: "specificationgroupsid");

            migrationBuilder.CreateIndex(
                name: "IX_specificationvalues_creatorId",
                table: "specificationvalues",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_specificationvalues_lastmodifierId",
                table: "specificationvalues",
                column: "lastmodifierId");

            migrationBuilder.CreateIndex(
                name: "IX_specificationvalues_productId",
                table: "specificationvalues",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_specificationvalues_specificationid",
                table: "specificationvalues",
                column: "specificationid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "specificationvalues");

            migrationBuilder.DropTable(
                name: "specifications");

            migrationBuilder.DropTable(
                name: "specificationgroups");
        }
    }
}
