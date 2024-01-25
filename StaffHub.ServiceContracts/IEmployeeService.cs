using StaffHub.ServiceContracts.DTO;
using StaffHub.ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffHub.ServiceContracts
{
    public interface IEmployeeService
    {
        EmployeeResponse AddEmployee(EmployeeAddRequest? request);
        List<EmployeeResponse> GetAllEmployees();
        EmployeeResponse GetEmployeeByID(Guid? employeeID);
        List<EmployeeResponse> GetFilteredEmployees(string searchBy, string? searchString);
        List<EmployeeResponse> GetSortedEmployees(
            List<EmployeeResponse> allEmployee, string sortBy, SortOrderOptions sortOrder);
        EmployeeResponse UpdateEmployees(EmployeeUpdateRequest employeeUpdateRequest);
        bool DeleteEmployee(Guid? employeeID);

    }

}
