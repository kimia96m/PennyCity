using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class stinggtobrforeasint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "lefteddays",
                table: "specialprodcut",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "lefteddays",
                table: "specialprodcut",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
