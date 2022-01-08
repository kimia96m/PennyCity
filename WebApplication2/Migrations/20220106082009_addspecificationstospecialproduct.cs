using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addspecificationstospecialproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecialProductsid",
                table: "specificationvalues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_specificationvalues_SpecialProductsid",
                table: "specificationvalues",
                column: "SpecialProductsid");

            migrationBuilder.AddForeignKey(
                name: "FK_specificationvalues_specialprodcut_SpecialProductsid",
                table: "specificationvalues",
                column: "SpecialProductsid",
                principalTable: "specialprodcut",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_specificationvalues_specialprodcut_SpecialProductsid",
                table: "specificationvalues");

            migrationBuilder.DropIndex(
                name: "IX_specificationvalues_SpecialProductsid",
                table: "specificationvalues");

            migrationBuilder.DropColumn(
                name: "SpecialProductsid",
                table: "specificationvalues");
        }
    }
}
