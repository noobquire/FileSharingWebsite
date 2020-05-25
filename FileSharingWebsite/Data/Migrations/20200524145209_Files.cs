using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSharingWebsite.Data.Migrations
{
    public partial class Files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    DateUploaded = table.Column<DateTime>(nullable: false),
                    Downloads = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    Extension = table.Column<string>(maxLength: 20, nullable: true),
                    MediaType = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
