using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class additemproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tags_tags_Tagsid",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "IX_tags_Tagsid",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "Tagsid",
                table: "tags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tagsid",
                table: "tags",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tags_Tagsid",
                table: "tags",
                column: "Tagsid");

            migrationBuilder.AddForeignKey(
                name: "FK_tags_tags_Tagsid",
                table: "tags",
                column: "Tagsid",
                principalTable: "tags",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
