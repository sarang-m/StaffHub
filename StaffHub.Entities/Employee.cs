using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.Entities
{
    public class Employee
    {
        [Key]
        public Guid EmployeeID { get; set; }
        [StringLength(50)]
        public string? EmployeeName { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(10)]
        public string? Gender { get; set; }
        public Guid? DepartmentID { get; set; }
        [StringLength(40)]
        public string? Role { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("DepartmentID")]
        public virtual Department? Department { get; set; }
    }
}
