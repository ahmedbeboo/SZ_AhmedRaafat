using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_AhmedRaafat_Repository.Migrations
{
    public partial class add_FK_In_Table_Item : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "userId",
                table: "ToDo",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "userId",
                table: "PersonalDiaries",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_userId",
                table: "ToDo",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDiaries_userId",
                table: "PersonalDiaries",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalDiaries_AspNetUsers_userId",
                table: "PersonalDiaries",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDo_AspNetUsers_userId",
                table: "ToDo",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalDiaries_AspNetUsers_userId",
                table: "PersonalDiaries");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDo_AspNetUsers_userId",
                table: "ToDo");

            migrationBuilder.DropIndex(
                name: "IX_ToDo_userId",
                table: "ToDo");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDiaries_userId",
                table: "PersonalDiaries");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "ToDo");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "PersonalDiaries");
        }
    }
}
