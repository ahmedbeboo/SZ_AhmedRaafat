using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_AhmedRaafat_Repository.Migrations
{
    public partial class modifyPropertyFName_LNameToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LName",
                table: "AspNetUsers",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FName",
                table: "AspNetUsers",
                newName: "firstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "AspNetUsers",
                newName: "LName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "AspNetUsers",
                newName: "FName");
        }
    }
}
