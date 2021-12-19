using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class ispecialbool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bannergroups_AspNetUsers_creatorId",
                table: "bannergroups");

            migrationBuilder.DropForeignKey(
                name: "FK_bannergroups_AspNetUsers_lastmodifierId",
                table: "bannergroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bannergroups",
                table: "bannergroups");

            migrationBuilder.RenameTable(
                name: "bannergroups",
                newName: "bannergroup");

            migrationBuilder.RenameIndex(
                name: "IX_bannergroups_lastmodifierId",
                table: "bannergroup",
                newName: "IX_bannergroup_lastmodifierId");

            migrationBuilder.RenameIndex(
                name: "IX_bannergroups_creatorId",
                table: "bannergroup",
                newName: "IX_bannergroup_creatorId");

            migrationBuilder.AddColumn<string>(
                name: "ext",
                table: "banners",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ispecial",
                table: "banners",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_bannergroup",
                table: "bannergroup",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_bannergroup_AspNetUsers_creatorId",
                table: "bannergroup",
                column: "creatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bannergroup_AspNetUsers_lastmodifierId",
                table: "bannergroup",
                column: "lastmodifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bannergroup_AspNetUsers_creatorId",
                table: "bannergroup");

            migrationBuilder.DropForeignKey(
                name: "FK_bannergroup_AspNetUsers_lastmodifierId",
                table: "bannergroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bannergroup",
                table: "bannergroup");

            migrationBuilder.DropColumn(
                name: "ext",
                table: "banners");

            migrationBuilder.DropColumn(
                name: "ispecial",
                table: "banners");

            migrationBuilder.RenameTable(
                name: "bannergroup",
                newName: "bannergroups");

            migrationBuilder.RenameIndex(
                name: "IX_bannergroup_lastmodifierId",
                table: "bannergroups",
                newName: "IX_bannergroups_lastmodifierId");

            migrationBuilder.RenameIndex(
                name: "IX_bannergroup_creatorId",
                table: "bannergroups",
                newName: "IX_bannergroups_creatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bannergroups",
                table: "bannergroups",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_bannergroups_AspNetUsers_creatorId",
                table: "bannergroups",
                column: "creatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bannergroups_AspNetUsers_lastmodifierId",
                table: "bannergroups",
                column: "lastmodifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
