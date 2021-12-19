using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class enn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productitems_tagvalues_tagvaluesid",
                table: "productitems");

            migrationBuilder.DropIndex(
                name: "IX_productitems_tagvaluesid",
                table: "productitems");

            migrationBuilder.DropColumn(
                name: "tagvaluesid",
                table: "productitems");

            migrationBuilder.CreateTable(
                name: "itemtagvalues",
                columns: table => new
                {
                    tagvalueid = table.Column<int>(nullable: false),
                    productitemid = table.Column<int>(nullable: false),
                    tagvaluesid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemtagvalues", x => new { x.tagvalueid, x.productitemid });
                    table.ForeignKey(
                        name: "FK_itemtagvalues_productitems_productitemid",
                        column: x => x.productitemid,
                        principalTable: "productitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itemtagvalues_tagvalues_tagvaluesid",
                        column: x => x.tagvaluesid,
                        principalTable: "tagvalues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_itemtagvalues_productitemid",
                table: "itemtagvalues",
                column: "productitemid");

            migrationBuilder.CreateIndex(
                name: "IX_itemtagvalues_tagvaluesid",
                table: "itemtagvalues",
                column: "tagvaluesid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itemtagvalues");

            migrationBuilder.AddColumn<int>(
                name: "tagvaluesid",
                table: "productitems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_productitems_tagvaluesid",
                table: "productitems",
                column: "tagvaluesid");

            migrationBuilder.AddForeignKey(
                name: "FK_productitems_tagvalues_tagvaluesid",
                table: "productitems",
                column: "tagvaluesid",
                principalTable: "tagvalues",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
