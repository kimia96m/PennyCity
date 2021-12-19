using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addgg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itemtagvalues_tagvalues_tagvaluesid",
                table: "itemtagvalues");

            migrationBuilder.DropIndex(
                name: "IX_itemtagvalues_tagvaluesid",
                table: "itemtagvalues");

            migrationBuilder.DropColumn(
                name: "tagvaluesid",
                table: "itemtagvalues");

            migrationBuilder.AddForeignKey(
                name: "FK_itemtagvalues_tagvalues_tagvalueid",
                table: "itemtagvalues",
                column: "tagvalueid",
                principalTable: "tagvalues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itemtagvalues_tagvalues_tagvalueid",
                table: "itemtagvalues");

            migrationBuilder.AddColumn<int>(
                name: "tagvaluesid",
                table: "itemtagvalues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_itemtagvalues_tagvaluesid",
                table: "itemtagvalues",
                column: "tagvaluesid");

            migrationBuilder.AddForeignKey(
                name: "FK_itemtagvalues_tagvalues_tagvaluesid",
                table: "itemtagvalues",
                column: "tagvaluesid",
                principalTable: "tagvalues",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
