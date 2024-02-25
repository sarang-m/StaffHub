using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.ServiceContracts.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="Name can't be blank")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Invalid emial")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password can't be blank")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
