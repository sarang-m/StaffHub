using StaffHub.Entities;
using StaffHub.ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.ServiceContracts.DTO
{
    public class EmployeeAddRequest
    {
        [Required(ErrorMessage = "Employee name can't be empty")]
        public string? EmployeeName { get; set; }
        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? DepartmentID { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; } = true;

        public Employee ToEmployee()
        {
            return new Employee() { 
                EmployeeName = this.EmployeeName, 
                Email = this.Email, 
                DateOfBirth = this.DateOfBirth, 
                Gender = this.Gender.ToString(), 
                DepartmentID = this.DepartmentID,
                Role = this.Role, 
                IsActive= this.IsActive };
        }
    }
    
}
