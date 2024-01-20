using StaffHub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.ServiceContracts.DTO
{
    public class DepartmentAddRequest
    {
        public string? DepartmentName { get; set; }

        public Department ToDepartment()
        {
            return new Department() { DepartmenName = this.DepartmentName };
    }
    }

    
}
