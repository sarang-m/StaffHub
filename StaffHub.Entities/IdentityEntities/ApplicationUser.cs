using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace StaffHub.Entities.IdentityEntities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string? EmployeeName {  get; set; }
    }
}
