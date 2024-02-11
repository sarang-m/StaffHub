using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.Entities
{
    public class EmployeesDbContext:DbContext
    {
        public EmployeesDbContext(DbContextOptions options): base(options)
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
                modelBuilder.Entity<Employee>().HasData(department);
            }

            //seed to employees
            string employeeJson = System.IO.File.ReadAllText("employees.json");
            List<Employee> employees = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(employeeJson);

            foreach (Employee employee in employees)
            {
                modelBuilder.Entity<Employee>().HasData(employee);
            }

        }
    }
}
