using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSharingWebsite.Data.Migrations
{
    public partial class AuthorAsStringId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_AspNetUsers_AuthorId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_AuthorId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Files",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_AuthorId",
                table: "Files",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_AspNetUsers_AuthorId",
                table: "Files",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
