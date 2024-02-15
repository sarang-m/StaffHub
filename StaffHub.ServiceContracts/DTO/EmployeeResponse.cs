using StaffHub.Entities;
using StaffHub.ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.ServiceContracts.DTO
{
    public class EmployeeResponse
    {
        public Guid EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
        public double? Age { get; set; }

        public EmployeeUpdateRequest ToEmployeeUpdateRequest()
        {
            return new EmployeeUpdateRequest()
            {
                EmployeeID = this.EmployeeID,
                EmployeeName = this.EmployeeName,
                Email = this.Email,
                DateOfBirth = this.DateOfBirth,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), this.Gender, true),
                DepartmentID = this.DepartmentID,
                Role = this.Role,
                IsActive = this.IsActive
            };
        }
    }

    public static class EmployeeExtension
    {
        public static EmployeeResponse ToEmployeeResponse(this Employee employee)
        {
            EmployeeResponse employeeResponse = new EmployeeResponse() { 
                EmployeeID = employee.EmployeeID, 
                EmployeeName = employee.EmployeeName, 
                Email = employee.Email, 
                DateOfBirth = employee.DateOfBirth, 
                Gender = employee.Gender, 
                DepartmentID = employee.DepartmentID, 
                DepartmentName = employee.Department?.DepartmenName,
                Role = employee.Role, 
                IsActive = employee.IsActive, 
                Age = (employee.DateOfBirth != null)? 
                Math.Round((DateTime.Now - employee.DateOfBirth.Value).TotalDays/ 365.25): null, };

            return employeeResponse;

        }
    }
}
