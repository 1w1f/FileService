using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileServiceRepsitory.Migrations
{
    public partial class FkInloginRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginRecords_Users_UserDtoId",
                table: "LoginRecords");

            migrationBuilder.DropIndex(
                name: "IX_LoginRecords_UserDtoId",
                table: "LoginRecords");

            migrationBuilder.DropColumn(
                name: "UserDtoId",
                table: "LoginRecords");

            migrationBuilder.CreateIndex(
                name: "IX_LoginRecords_UserId",
                table: "LoginRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginRecords_Users_UserId",
                table: "LoginRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginRecords_Users_UserId",
                table: "LoginRecords");

            migrationBuilder.DropIndex(
                name: "IX_LoginRecords_UserId",
                table: "LoginRecords");

            migrationBuilder.AddColumn<int>(
                name: "UserDtoId",
                table: "LoginRecords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginRecords_UserDtoId",
                table: "LoginRecords",
                column: "UserDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginRecords_Users_UserDtoId",
                table: "LoginRecords",
                column: "UserDtoId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
