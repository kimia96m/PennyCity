using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addusertocomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userid",
                table: "comments",
                newName: "userId");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "comments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_comments_userId",
                table: "comments",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_userId",
                table: "comments",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_userId",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_userId",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "comments",
                newName: "userid");

            migrationBuilder.AlterColumn<int>(
                name: "userid",
                table: "comments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
