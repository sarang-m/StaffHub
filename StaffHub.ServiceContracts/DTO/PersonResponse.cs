using StaffHub.Entities;
using StaffHub.ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.ServiceContracts.DTO
{
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
        public double? Age { get; set; }

        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                PersonId = this.PersonID,
                PersonName = this.PersonName,
                Email = this.Email,
                DateOfBirth = this.DateOfBirth,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), this.Gender, true),
                DepartmentID = this.DepartmentID,
                Role = this.Role,
                IsActive = this.IsActive
            };
        }
    }

    public static class PersonExtension
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            PersonResponse personResponse = new PersonResponse() { 
                PersonID = person.PersonID, 
                PersonName = person.PersonName, 
                Email = person.Email, 
                DateOfBirth = person.DateOfBirth, 
                Gender = person.Gender, 
                DepartmentID = person.DepartmentID, 
                Role = person.Role, 
                IsActive = person.IsActive, 
                Age = (person.DateOfBirth != null)? 
                Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays/ 365.25): null, };

            return personResponse;

        }
    }
}
