using StaffHub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.ServiceContracts.DTO
{
    public class DepartmentResponse
    {
        public Guid DeparmentId { get; set; }
        public string? DepartmentName { get; set; }
    }

    public static class DepartmentExtension
    {
        public static DepartmentResponse ToDepartmentResponse(this Department department)
        {
            return new DepartmentResponse()
            {
                DeparmentId = department.DepartmentId,
                DepartmentName = department.DepartmenName
            };
        }
    }
}
