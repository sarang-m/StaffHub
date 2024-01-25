using StaffHub.Entities;
using StaffHub.ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;
namespace StaffHub.ServiceContracts.DTO
{
    public class EmployeeUpdateRequest
    {
        [Required(ErrorMessage = "Employee ID can't be empty")]
        public Guid EmployeeID { get; set; }
        [Required(ErrorMessage = "Employee name can't be empty")]
        public string? EmployeeName { get; set; }
        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? DepartmentID { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; } = true;

        public Employee ToEmployee()
        {
            return new Employee()
            {
                EmployeeID = this.EmployeeID,
                EmployeeName = this.EmployeeName,
                Email = this.Email,
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender.ToString(),
                DepartmentID = this.DepartmentID,
                Role = this.Role,
                IsActive = this.IsActive
            };
        }
    }
}
