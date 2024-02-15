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
        Task<EmployeeResponse> AddEmployee(EmployeeAddRequest? request);
        Task<List<EmployeeResponse>> GetAllEmployees();
        Task<EmployeeResponse> GetEmployeeByID(Guid? employeeID);
        Task<List<EmployeeResponse>> GetFilteredEmployees(string searchBy, string? searchString);
        Task<List<EmployeeResponse>> GetSortedEmployees(
            List<EmployeeResponse> allEmployee, string sortBy, SortOrderOptions sortOrder);
        Task<EmployeeResponse> UpdateEmployees(EmployeeUpdateRequest employeeUpdateRequest);
        Task<bool> DeleteEmployee(Guid? employeeID);

    }

}
