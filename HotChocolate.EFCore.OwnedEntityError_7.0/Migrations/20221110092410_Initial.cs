using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotChocolate.EFCore.OwnedEntityError7._0.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ConsignorName = table.Column<string>(name: "Consignor_Name", type: "TEXT", nullable: true),
                    ConsignorAddressLine1 = table.Column<string>(name: "Consignor_AddressLine1", type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shipments");
        }
    }
}
