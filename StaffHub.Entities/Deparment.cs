using System.ComponentModel.DataAnnotations;

namespace StaffHub.Entities
{
    //Domain Model for Department
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }
        public string? DepartmenName { get; set; }
    }
}