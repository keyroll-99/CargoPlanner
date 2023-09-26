using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Modules.Companies.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_is_active_to_employee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Companies_CompanyId",
                schema: "companies",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                schema: "companies",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Employee",
                schema: "companies",
                newName: "Employees",
                newSchema: "companies");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_CompanyId",
                schema: "companies",
                table: "Employees",
                newName: "IX_Employees_CompanyId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "companies",
                table: "Employees",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                schema: "companies",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                schema: "companies",
                table: "Employees",
                column: "CompanyId",
                principalSchema: "companies",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                schema: "companies",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                schema: "companies",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "companies",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                schema: "companies",
                newName: "Employee",
                newSchema: "companies");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CompanyId",
                schema: "companies",
                table: "Employee",
                newName: "IX_Employee_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                schema: "companies",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Companies_CompanyId",
                schema: "companies",
                table: "Employee",
                column: "CompanyId",
                principalSchema: "companies",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
