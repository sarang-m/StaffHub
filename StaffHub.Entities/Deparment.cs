using System;
using System.ComponentModel.DataAnnotations;

namespace StaffHub.Entities
{
    //Domain Model for Department
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }
        [StringLength(50)]
        public string? DepartmenName { get; set; }
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}