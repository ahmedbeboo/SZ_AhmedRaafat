using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_AhmedRaafat_Repository.Migrations
{
    public partial class addPropertyConfirmedToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "confirmed",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "confirmed",
                table: "AspNetUsers");
        }
    }
}
