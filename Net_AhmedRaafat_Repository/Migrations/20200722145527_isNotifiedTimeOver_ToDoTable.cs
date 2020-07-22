using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_AhmedRaafat_Repository.Migrations
{
    public partial class isNotifiedTimeOver_ToDoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isNotifiedTimeOver",
                table: "ToDo",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isNotifiedTimeOver",
                table: "ToDo");
        }
    }
}
