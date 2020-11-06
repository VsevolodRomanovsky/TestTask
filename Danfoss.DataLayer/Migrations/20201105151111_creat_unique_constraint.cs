using Microsoft.EntityFrameworkCore.Migrations;

namespace Danfoss.DataLayer.Migrations
{
    public partial class creat_unique_constraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine",
                table: "House");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "House",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "House",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_House_Street_HouseNumber",
                table: "House",
                columns: new[] { "Street", "HouseNumber" },
                unique: true,
                filter: "[Street] IS NOT NULL AND [HouseNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_House_Street_HouseNumber",
                table: "House");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "House",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "House",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine",
                table: "House",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
