using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Modules.Locations.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_addreses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                schema: "locations",
                table: "Locations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
                    CityDistrict = table.Column<string>(type: "text", nullable: true),
                    Continent = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    CountryCode = table.Column<string>(type: "text", nullable: true),
                    HouseNumber = table.Column<string>(type: "text", nullable: true),
                    PostCode = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AddressId",
                schema: "locations",
                table: "Locations",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Addresses_AddressId",
                schema: "locations",
                table: "Locations",
                column: "AddressId",
                principalSchema: "locations",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Addresses_AddressId",
                schema: "locations",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_AddressId",
                schema: "locations",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "locations",
                table: "Locations");
        }
    }
}
