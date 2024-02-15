using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffHub.Entities.Migrations
{
    public partial class InsertEmployee_SP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_InsertEmployees = @"
                CREATE PROCEDURE [dbo].[InsertEmployee]
                (@EmployeeID uniqueidentifier, @EmployeeName nvarchar(50), @Email nvarchar(100), 
                 @DateOfBirth datetime2(7), @Gender nvarchar(10), @DepartmentID uniqueidentifier, 
                 @Role nvarchar(40), @IsActive bit)
                AS BEGIN
                    INSERT INTO [dbo].[Employees] (EmployeeID, EmployeeName, Email, DateOfBirth, 
                    Gender, DepartmentID,Role, IsActive) VALUES (
                    @EmployeeID, @EmployeeName, @Email, @DateOfBirth, @Gender, @DepartmentID, @Role,
                    @IsActive)
                END
                ";
            migrationBuilder.Sql(sp_InsertEmployees);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_InsertEmployees = @"
                DROP PROCEDURE [dbo].[InsertEmployee]
            ";
            migrationBuilder.Sql(sp_InsertEmployees);
        }
    }
}
