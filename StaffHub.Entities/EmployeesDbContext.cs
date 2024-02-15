using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System;

namespace StaffHub.Entities
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Employee>().ToTable("Employees");

            //seed to department
            string departmentsJson = System.IO.File.ReadAllText("department.json");
            List<Department> departments = System.Text.Json.JsonSerializer.Deserialize<List<Department>>(departmentsJson);

            foreach (Department department in departments)
            {
                modelBuilder.Entity<Department>().HasData(department);
            }

            //seed to employees
            string employeeJson = System.IO.File.ReadAllText("employees.json");
            List<Employee> employees = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(employeeJson);

            foreach (Employee employee in employees)
            {
                modelBuilder.Entity<Employee>().HasData(employee);
            }

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne<Department>(d => d.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.DepartmentID);
            });



        }
        public List<Employee> sp_GetAllEmployees()
        {
            return Employees.FromSqlRaw("EXECUTE [dbo].[GetAllEmployees]").ToList();

        }

        public int sp_InsertEmployee(Employee employee)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@EmployeeID", employee.EmployeeID),
                new SqlParameter("@EmployeeName", employee.EmployeeName),
                new SqlParameter("@Email", employee.Email),
                new SqlParameter("@DateOfBirth", employee.DateOfBirth),
                new SqlParameter("@Gender", employee.Gender),
                new SqlParameter("@DepartmentID", employee.DepartmentID),
                new SqlParameter("@Role", employee.Role),
                new SqlParameter("@IsActive", employee.IsActive),
            };
            return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertEmployee] @EmployeeID, @EmployeeName,@Email, @DateOfBirth, @Gender, @DepartmentID, @Role, @IsActive", parameters);

        }
    }
}
