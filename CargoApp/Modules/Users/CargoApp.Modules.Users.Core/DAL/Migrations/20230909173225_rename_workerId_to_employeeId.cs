using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Modules.Users.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class rename_workerId_to_employeeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkerId",
                schema: "users",
                table: "Users",
                newName: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                schema: "users",
                table: "Users",
                newName: "WorkerId");
        }
    }
}
