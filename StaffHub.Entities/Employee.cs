using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.Entities
{
    public class Employee
    {
        public Guid EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? DepartmentID { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
    }
}
