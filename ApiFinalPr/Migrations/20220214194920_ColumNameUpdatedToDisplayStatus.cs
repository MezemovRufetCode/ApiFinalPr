using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiFinalPr.Migrations
{
    public partial class ColumNameUpdatedToDisplayStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "DisplayStatus",
                table: "Books",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayStatus",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
