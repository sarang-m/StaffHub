using StaffHub.ServiceContracts.DTO;
using StaffHub.ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.ServiceContracts
{
    public interface IPersonService
    {
        PersonResponse Addperson(PersonAddRequest? request);
        List<PersonResponse> GetAllPerson();
        PersonResponse GetPersonByID(Guid? personId);
        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);
        List<PersonResponse> GetSortedPersons(
            List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);
        PersonResponse UpdatePerson(PersonUpdateRequest personUpdateRequest);
        bool DeletePerson(Guid? personId);

    }

}
