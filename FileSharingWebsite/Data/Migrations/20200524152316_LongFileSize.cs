using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSharingWebsite.Data.Migrations
{
    public partial class LongFileSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "Files",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "Files",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
