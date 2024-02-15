using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffHub.Entities.Migrations
{
    public partial class GetAllEmployees_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllEmployees = @"
                CREATE PROCEDURE [dbo].[GetAllEmployees]
                AS BEGIN
                    SELECT EmployeeID, EmployeeName, Email, DateOfBirth, Gender, DepartmentID,
                    Role, IsActive FROM [dbo].[Employees]
                END
                ";
            migrationBuilder.Sql(sp_GetAllEmployees);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllEmployees = @"
                DROP PROCEDURE [dbo].[GetAllEmployees]
            ";
            migrationBuilder.Sql(sp_GetAllEmployees);

        }
    }
}
