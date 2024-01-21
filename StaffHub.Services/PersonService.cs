using StaffHub.Entities;
using StaffHub.ServiceContracts;
using StaffHub.ServiceContracts.DTO;
using StaffHub.ServiceContracts.Enums;
using StaffHub.Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.Services
{
    public class PersonService : IPersonService
    {
        private readonly List<Person> _persons;
        private readonly IDepartmentService _departmentService;

        public PersonService()
        {
            _persons = new List<Person>();
            _departmentService = new DepartmentService();
        }
        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            DepartmentResponse department = _departmentService.GetDepartmentByID(person.DepartmentID);

            if (department != null) personResponse.DepartmentName = department.DepartmentName;
            else personResponse.DepartmentName = null;
            return personResponse;

        }
        public PersonResponse Addperson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null) throw new ArgumentNullException(nameof(personAddRequest));

            //Model validation
            ValidationHelper.ModelValidation(personAddRequest);

            Person person = personAddRequest.ToPerson();
            person.PersonID = Guid.NewGuid();
            _persons.Add(person);

            return ConvertPersonToPersonResponse(person);
        }
            
        public List<PersonResponse> GetAllPerson()
        {
            List<PersonResponse> personResponses = _persons.Select((temp) => temp.ToPersonResponse()).ToList();
            return personResponses;
        }

        public PersonResponse GetPersonByID(Guid? personId)
        {
            if (personId == null) return null;

            Person? person = _persons.FirstOrDefault((temp) => temp.PersonID == personId);
            if (person == null) return null;

            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons= GetAllPerson();
            List<PersonResponse> matchingPersons = new List<PersonResponse>();

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            {
                return matchingPersons;
            }
            

            switch (searchBy)
            {
                case nameof(Person.PersonName):
                    matchingPersons = allPersons.Where(
                        (temp) => (!string.IsNullOrEmpty(temp.PersonName)) ? temp.PersonName.Contains(
                            searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(
                        (temp) => (!string.IsNullOrEmpty(temp.Email))?temp.Email.Contains(
                            searchString, StringComparison.OrdinalIgnoreCase): true).ToList();
                    break;

                case nameof(Person.DateOfBirth):
                    matchingPersons = allPersons.Where(
                        (temp) => (temp.DateOfBirth != null) ? temp.DateOfBirth.Value.ToString("dd MM yyyy").Contains(
                            searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(
                        (temp) => (!string.IsNullOrEmpty(temp.Gender)) ? temp.Gender.Contains(
                            searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(Person.DepartmentID):
                    matchingPersons = allPersons.Where(
                        (temp) => (!string.IsNullOrEmpty(temp.DepartmentName)) ? temp.DepartmentName.Contains(
                            searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(Person.Role):
                    matchingPersons = allPersons.Where(
                        (temp) => (!string.IsNullOrEmpty(temp.Role)) ? temp.Role.Contains(
                            searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                default:
                    matchingPersons = allPersons; break;
            }
            return matchingPersons;

        }

        public List<PersonResponse> GetSortedPersons(
            List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy)) return allPersons;

            List<PersonResponse> sortedList = new List<PersonResponse>();

            switch (sortBy)
            {
                case nameof(PersonResponse.PersonName):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList =allPersons.OrderBy(
                            temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    else
                    {
                        sortedList = allPersons.OrderByDescending(
                            temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    break;

                case nameof(PersonResponse.Email):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allPersons.OrderBy(
                            temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    else
                    {
                        sortedList = allPersons.OrderByDescending(
                            temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    break;
                case nameof(PersonResponse.DateOfBirth):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allPersons.OrderBy(
                            temp => temp.DateOfBirth).ToList();
                    }
                    else
                    {
                        sortedList = allPersons.OrderByDescending(
                            temp => temp.DateOfBirth).ToList();
                    }
                    break;
                case nameof(PersonResponse.Age):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allPersons.OrderBy(
                            temp => temp.Age).ToList();
                    }
                    else
                    {
                        sortedList = allPersons.OrderByDescending(
                            temp => temp.Age).ToList();
                    }
                    break;
                case nameof(PersonResponse.Gender):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allPersons.OrderBy(
                            temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    else
                    {
                        sortedList = allPersons.OrderByDescending(
                            temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    break;
                case nameof(PersonResponse.DepartmentName):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allPersons.OrderBy(
                            temp => temp.DepartmentName, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    else
                    {
                        sortedList = allPersons.OrderByDescending(
                            temp => temp.DepartmentName, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    break;
                case nameof(PersonResponse.Role):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allPersons.OrderBy(
                            temp => temp.Role, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    else
                    {
                        sortedList = allPersons.OrderByDescending(
                            temp => temp.Role, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    break;
                case nameof(PersonResponse.IsActive):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allPersons.OrderBy(
                            temp => temp.IsActive).ToList();
                    }
                    else
                    {
                        sortedList = allPersons.OrderByDescending(
                            temp => temp.IsActive).ToList();
                    }
                    break;

                default:
                    return allPersons;
            }
            return sortedList;

        }

        public PersonResponse UpdatePerson(PersonUpdateRequest personUpdateRequest)
        {
            if (personUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(personUpdateRequest));
            }
            ValidationHelper.ModelValidation(personUpdateRequest);
            Person? matchingPerson = _persons.FirstOrDefault(
                (temp) => temp.PersonID == personUpdateRequest.PersonId);
            if (matchingPerson == null)
            {
                throw new ArgumentException("Given person doesn't exist");
            }

            //Update the matching person details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.DepartmentID = personUpdateRequest.DepartmentID;
            matchingPerson.Role = personUpdateRequest.Role;
            matchingPerson.IsActive = personUpdateRequest.IsActive;

            return matchingPerson.ToPersonResponse();
        }

        public bool DeletePerson(Guid? personId)
        {
            if (personId == null) throw new ArgumentNullException(nameof(personId));

            Person? person = _persons.FirstOrDefault((temp) => temp.PersonID == personId);
            if (person == null)
            {
                return false;
            }
            _persons.RemoveAll((temp) => temp.PersonID == personId);
            return true;
        }
    }
}
