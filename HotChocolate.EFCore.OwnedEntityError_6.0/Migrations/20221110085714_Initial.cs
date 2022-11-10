using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotChoclate.EFCore.OwnedEntityError.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Consignor_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Consignor_AddressLine1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shipments");
        }
    }
}
