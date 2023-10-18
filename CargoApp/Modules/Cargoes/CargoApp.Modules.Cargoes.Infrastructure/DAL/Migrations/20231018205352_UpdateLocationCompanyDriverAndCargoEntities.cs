using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLocationCompanyDriverAndCargoEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_Locations__fromId",
                schema: "cargoes",
                table: "Cargoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_Locations__toId",
                schema: "cargoes",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                schema: "cargoes",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                schema: "cargoes",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                schema: "cargoes",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "_toId",
                schema: "cargoes",
                table: "Cargoes",
                newName: "LocationToId");

            migrationBuilder.RenameColumn(
                name: "_fromId",
                schema: "cargoes",
                table: "Cargoes",
                newName: "LocationFromId");

            migrationBuilder.RenameIndex(
                name: "IX_Cargoes__toId",
                schema: "cargoes",
                table: "Cargoes",
                newName: "IX_Cargoes_LocationToId");

            migrationBuilder.RenameIndex(
                name: "IX_Cargoes__fromId",
                schema: "cargoes",
                table: "Cargoes",
                newName: "IX_Cargoes_LocationFromId");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "cargoes",
                table: "Drivers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HomeId",
                schema: "cargoes",
                table: "Drivers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "_isActive",
                schema: "cargoes",
                table: "Drivers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "cargoes",
                table: "Companies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                schema: "cargoes",
                table: "Companies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CompanyId",
                schema: "cargoes",
                table: "Drivers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_HomeId",
                schema: "cargoes",
                table: "Drivers",
                column: "HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_Locations_LocationFromId",
                schema: "cargoes",
                table: "Cargoes",
                column: "LocationFromId",
                principalSchema: "cargoes",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_Locations_LocationToId",
                schema: "cargoes",
                table: "Cargoes",
                column: "LocationToId",
                principalSchema: "cargoes",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Companies_CompanyId",
                schema: "cargoes",
                table: "Drivers",
                column: "CompanyId",
                principalSchema: "cargoes",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Locations_HomeId",
                schema: "cargoes",
                table: "Drivers",
                column: "HomeId",
                principalSchema: "cargoes",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_Locations_LocationFromId",
                schema: "cargoes",
                table: "Cargoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_Locations_LocationToId",
                schema: "cargoes",
                table: "Cargoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Companies_CompanyId",
                schema: "cargoes",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Locations_HomeId",
                schema: "cargoes",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_CompanyId",
                schema: "cargoes",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_HomeId",
                schema: "cargoes",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "cargoes",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "HomeId",
                schema: "cargoes",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "_isActive",
                schema: "cargoes",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "cargoes",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                schema: "cargoes",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "LocationToId",
                schema: "cargoes",
                table: "Cargoes",
                newName: "_toId");

            migrationBuilder.RenameColumn(
                name: "LocationFromId",
                schema: "cargoes",
                table: "Cargoes",
                newName: "_fromId");

            migrationBuilder.RenameIndex(
                name: "IX_Cargoes_LocationToId",
                schema: "cargoes",
                table: "Cargoes",
                newName: "IX_Cargoes__toId");

            migrationBuilder.RenameIndex(
                name: "IX_Cargoes_LocationFromId",
                schema: "cargoes",
                table: "Cargoes",
                newName: "IX_Cargoes__fromId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                schema: "cargoes",
                table: "Locations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                schema: "cargoes",
                table: "Drivers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                schema: "cargoes",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_Locations__fromId",
                schema: "cargoes",
                table: "Cargoes",
                column: "_fromId",
                principalSchema: "cargoes",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_Locations__toId",
                schema: "cargoes",
                table: "Cargoes",
                column: "_toId",
                principalSchema: "cargoes",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
