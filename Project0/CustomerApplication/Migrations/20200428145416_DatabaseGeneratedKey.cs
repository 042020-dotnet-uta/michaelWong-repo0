using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerApplication.Migrations
{
    public partial class DatabaseGeneratedKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Locations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Locations",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Locations",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Locations",
                newName: "id");
        }
    }
}
