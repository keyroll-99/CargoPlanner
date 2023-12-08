using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsDelivered_And_IsCanceled_To_Cargo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                schema: "cargoes",
                table: "Cargoes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                schema: "cargoes",
                table: "Cargoes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                schema: "cargoes",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "IsDelivered",
                schema: "cargoes",
                table: "Cargoes");
        }
    }
}
