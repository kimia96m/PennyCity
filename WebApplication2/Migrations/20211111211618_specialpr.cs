using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class specialpr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "specialprodcut",
                columns: table => new
                {
                    title = table.Column<string>(nullable: true),
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    brandid = table.Column<int>(nullable: true),
                    state = table.Column<byte>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    leftedtime = table.Column<DateTime>(nullable: true),
                    discount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialprodcut", x => x.id);
                    table.ForeignKey(
                        name: "FK_specialprodcut_Brand_brandid",
                        column: x => x.brandid,
                        principalTable: "Brand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_specialprodcut_brandid",
                table: "specialprodcut",
                column: "brandid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "specialprodcut");
        }
    }
}
