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
        [Required(ErrorMessage = "Please enter the employee name")]
        public string? EmployeeName { get; set; }

        [Required(ErrorMessage = "Please enter the email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter the Date of Birth")]
        [DataType(dataType:DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please Select the Gender")]
        public GenderOptions? Gender { get; set; }
        public Guid? DepartmentID { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; } 

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
