using Microsoft.EntityFrameworkCore.Migrations;

namespace Danfoss.DataLayer.Migrations
{
    public partial class create_schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counter",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(nullable: true),
                    Value = table.Column<long>(nullable: false),
                    HouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Counter_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Counter_HouseId",
                table: "Counter",
                column: "HouseId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counter");

            migrationBuilder.DropTable(
                name: "House");
        }
    }
}
