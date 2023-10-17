using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                schema: "cargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
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
                    OsmId = table.Column<long>(type: "bigint", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cargoes",
                schema: "cargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    _deliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _driverId = table.Column<Guid>(type: "uuid", nullable: true),
                    _expectedDeliveryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _fromId = table.Column<Guid>(type: "uuid", nullable: true),
                    _receiverId = table.Column<Guid>(type: "uuid", nullable: true),
                    _senderId = table.Column<Guid>(type: "uuid", nullable: true),
                    _toId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargoes_Companies__receiverId",
                        column: x => x._receiverId,
                        principalSchema: "cargoes",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cargoes_Companies__senderId",
                        column: x => x._senderId,
                        principalSchema: "cargoes",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cargoes_Drivers__driverId",
                        column: x => x._driverId,
                        principalSchema: "cargoes",
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cargoes_Locations__fromId",
                        column: x => x._fromId,
                        principalSchema: "cargoes",
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cargoes_Locations__toId",
                        column: x => x._toId,
                        principalSchema: "cargoes",
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes__driverId",
                schema: "cargoes",
                table: "Cargoes",
                column: "_driverId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes__fromId",
                schema: "cargoes",
                table: "Cargoes",
                column: "_fromId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes__receiverId",
                schema: "cargoes",
                table: "Cargoes",
                column: "_receiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes__senderId",
                schema: "cargoes",
                table: "Cargoes",
                column: "_senderId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes__toId",
                schema: "cargoes",
                table: "Cargoes",
                column: "_toId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargoes",
                schema: "cargoes");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "cargoes");

            migrationBuilder.DropTable(
                name: "Drivers",
                schema: "cargoes");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "cargoes");
        }
    }
}
