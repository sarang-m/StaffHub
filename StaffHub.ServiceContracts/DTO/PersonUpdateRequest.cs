﻿using StaffHub.Entities;
using StaffHub.ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;
namespace StaffHub.ServiceContracts.DTO
{
    public class PersonUpdateRequest
    {
        [Required(ErrorMessage ="Person ID can't be empty")]
        public Guid PersonId { get; set; }
        [Required(ErrorMessage = "Person name can't be empty")]
        public string? PersonName { get; set; }
        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? DepartmentID { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; } = true;

        public Person ToPerson()
        {
            return new Person()
            {
                PersonID = this.PersonId,
                PersonName = this.PersonName,
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
