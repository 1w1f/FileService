using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileServiceRepsitory.Migrations
{
    public partial class updateFileEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileEntitys",
                table: "FileEntitys");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "FileEntitys");

            migrationBuilder.RenameTable(
                name: "FileEntitys",
                newName: "Files");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Files",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OssFileName",
                table: "Files",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "OssFileName",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "FileEntitys");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "FileEntitys",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileEntitys",
                table: "FileEntitys",
                column: "FileId");
        }
    }
}
