using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffHub.Entities.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmenName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DepartmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentId", "DepartmenName" },
                values: new object[,]
                {
                    { new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), "Finance" },
                    { new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), "IT" },
                    { new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), "Sales" },
                    { new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), "HR" },
                    { new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), "Marketing" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "DateOfBirth", "DepartmentID", "Email", "EmployeeName", "Gender", "IsActive", "Role" },
                values: new object[,]
                {
                    { new Guid("02dfa2c0-27c2-49a4-a52f-99107bdf8f93"), new DateTime(1995, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), "ttregona4@stumbleupon.com", "Tani", "Gender", false, "HR Manager" },
                    { new Guid("05df0d9f-8431-4a4d-b349-7d6fae774150"), new DateTime(1990, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), "mconachya@va.gov", "Minta", "Female", true, "Sales Manager" },
                    { new Guid("1a35c0c8-ac6f-49fd-9740-fca801441b79"), new DateTime(1995, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), "fbowsher2@howstuffworks.com", "Franchot", "Male", true, "Developer" },
                    { new Guid("4662b347-1057-4d0f-98b5-4ecfb5c77edc"), new DateTime(1988, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), "mlingfoot5@netvibes.com", "Mitchael", "Male", false, "Content Creator" },
                    { new Guid("5c041832-af60-4048-b2eb-a309514752be"), new DateTime(1987, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), "asarvar3@dropbox.com", "Angie", "Male", true, "Accountant" },
                    { new Guid("75b97179-89dc-4830-bf72-6f854dc4d71a"), new DateTime(1987, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), "vklussb@nationalgeographic.com", "Verene", "Female", true, "Sales Representative" },
                    { new Guid("788075ef-ce02-4f88-8be0-bd70e67e80ae"), new DateTime(1990, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), "hmosco8@tripod.com", "Hansiain", "Male", true, "Accountant" },
                    { new Guid("85324c3e-7041-4ab1-b7c0-aa43fffec6d6"), new DateTime(1990, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), "ushears1@globo.com", "Ursa", "Female", false, "Developer" },
                    { new Guid("97c269b2-a785-48cb-a2d6-bbbdda264d94"), new DateTime(1997, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), "lwoodwing9@wix.com", "Lombard", "Male", false, "Content Creator" },
                    { new Guid("9b32b1ba-3586-40d0-8e76-8500e0a84689"), new DateTime(1989, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ec51293b-52de-43b5-8765-49a5d7325dc7"), "mwebsdale0@people.com.cn", "Marguerite", "Female", false, "Recruitment Specialist" },
                    { new Guid("c10089c8-e14c-4e70-8003-c2baede7cf95"), new DateTime(1998, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), "pretchford7@virginia.edu", "Pegeen", "Female", true, "Auditor" },
                    { new Guid("ca065236-a934-4e19-8e2f-cdfeeb187998"), new DateTime(1983, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), "mjarrell6@wisc.edu", "Maddy", "Male", true, "Financial Analyst" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
