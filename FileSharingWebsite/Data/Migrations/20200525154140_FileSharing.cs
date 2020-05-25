using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSharingWebsite.Data.Migrations
{
    public partial class FileSharing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Files",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "Files");
        }
    }
}
