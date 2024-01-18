using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RegenerateCargoesModulesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cargoes");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "cargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "cargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Lat = table.Column<double>(type: "double precision", nullable: false),
                    Lon = table.Column<double>(type: "double precision", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OsmId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                schema: "cargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    HomeId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Driver_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "cargoes",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Driver_Locations_HomeId",
                        column: x => x.HomeId,
                        principalSchema: "cargoes",
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cargoes",
                schema: "cargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationFromId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationToId = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExpectedDeliveryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDelivered = table.Column<bool>(type: "boolean", nullable: false),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargoes_Companies_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "cargoes",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cargoes_Companies_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "cargoes",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cargoes_Driver_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "cargoes",
                        principalTable: "Driver",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cargoes_Locations_LocationFromId",
                        column: x => x.LocationFromId,
                        principalSchema: "cargoes",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cargoes_Locations_LocationToId",
                        column: x => x.LocationToId,
                        principalSchema: "cargoes",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_DriverId",
                schema: "cargoes",
                table: "Cargoes",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_LocationFromId",
                schema: "cargoes",
                table: "Cargoes",
                column: "LocationFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_LocationToId",
                schema: "cargoes",
                table: "Cargoes",
                column: "LocationToId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_ReceiverId",
                schema: "cargoes",
                table: "Cargoes",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_SenderId",
                schema: "cargoes",
                table: "Cargoes",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_CompanyId",
                schema: "cargoes",
                table: "Driver",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_HomeId",
                schema: "cargoes",
                table: "Driver",
                column: "HomeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargoes",
                schema: "cargoes");

            migrationBuilder.DropTable(
                name: "Driver",
                schema: "cargoes");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "cargoes");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "cargoes");
        }
    }
}
