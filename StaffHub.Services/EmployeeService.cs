using Microsoft.EntityFrameworkCore;
using StaffHub.Entities;
using StaffHub.ServiceContracts;
using StaffHub.ServiceContracts.DTO;
using StaffHub.ServiceContracts.Enums;
using StaffHub.Services.Helpers;


namespace StaffHub.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeesDbContext _dbContext;
        private readonly IDepartmentService _departmentService;

        public EmployeeService(EmployeesDbContext employeesDbContext, IDepartmentService departmentService)
        {
            _dbContext = employeesDbContext;
            _departmentService = departmentService;
            
        }
        private async Task<EmployeeResponse> ConvertEmployeeToEmployeeResponse(Employee employee)
        {
            EmployeeResponse employeeResponse = employee.ToEmployeeResponse();
            DepartmentResponse department = await _departmentService.GetDepartmentByID(employee.DepartmentID);

            if (department != null) employeeResponse.DepartmentName = department.DepartmentName;
            else employeeResponse.DepartmentName = null;
            return employeeResponse;

        }
        public async Task<EmployeeResponse> AddEmployee(EmployeeAddRequest? employeeAddRequest)
        {
            if (employeeAddRequest == null) throw new ArgumentNullException(nameof(employeeAddRequest));

            //Model validation
            ValidationHelper.ModelValidation(employeeAddRequest);

            Employee employee = employeeAddRequest.ToEmployee();
            employee.EmployeeID = Guid.NewGuid();

            
            //_dbContext.sp_InsertEmployee(employee);
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();


            return await ConvertEmployeeToEmployeeResponse(employee);
        }
            
        public async Task<List<EmployeeResponse>> GetAllEmployees()
        {
            var employees = await _dbContext.Employees.Include("Department").ToListAsync();
            return employees
                .Select(temp => temp.ToEmployeeResponse()).ToList();

            //return _dbContext.sp_GetAllEmployees().Select(
            //    (temp) => ConvertEmployeeToEmployeeResponse(temp)).ToList();

            //List<EmployeeResponse> employeeResponses = _dbContext.Employees.ToList().Select(
            //    (temp) => ConvertEmployeeToEmployeeResponse(temp)).ToList();

            //List<Employee> employees = await _dbContext.Employees.ToListAsync();
            //List<Task<EmployeeResponse>> conversionTask = employees.Select(async (temp) => 
            //await ConvertEmployeeToEmployeeResponse(temp)).ToList();

            //EmployeeResponse[] employeeResponses = await Task.WhenAll(conversionTask);

            //return employeeResponses.ToList();

            //return employeeResponses;


        }

        public async Task<EmployeeResponse?> GetEmployeeByID(Guid? employeeID)
        {
            if (employeeID == null) return null;

            Employee? employee = await _dbContext.Employees.FirstOrDefaultAsync((temp) => temp.EmployeeID == employeeID);
            if (employee == null) return null;

            return await ConvertEmployeeToEmployeeResponse(employee);
        }

        public async Task<List<EmployeeResponse>> GetFilteredEmployees(string searchBy, string? searchString)
        {
            List<EmployeeResponse> allEmployees= await GetAllEmployees();
            List<EmployeeResponse> matchingEmployees = allEmployees;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            {
                return matchingEmployees;
            }
            

            switch (searchBy)
            {
                case nameof(EmployeeResponse.EmployeeName):
                    matchingEmployees = allEmployees.Where(
                        (temp) => (!string.IsNullOrEmpty(temp.EmployeeName)) ? temp.EmployeeName.Contains(
                            searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(EmployeeResponse.Email):
                    matchingEmployees = allEmployees.Where(
                        (temp) => (!string.IsNullOrEmpty(temp.Email)?temp.Email.Contains(
                            searchString, StringComparison.OrdinalIgnoreCase): true)).ToList();
                    break;

                case nameof(EmployeeResponse.DateOfBirth):
                    matchingEmployees = allEmployees.Where(
                        (temp) => (temp.DateOfBirth != null) ? temp.DateOfBirth.Value.ToString("dd MM yyyy").Contains(
                            searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(EmployeeResponse.Gender):
                    matchingEmployees = allEmployees.Where(
                        (temp) => (!string.IsNullOrEmpty(temp.Gender) ? temp.Gender.StartsWith(
                            searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(EmployeeResponse.DepartmentID):
                    matchingEmployees = allEmployees.Where(
                        (temp) => (!string.IsNullOrEmpty(temp.DepartmentName) ? temp.DepartmentName.Contains(
                            searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(EmployeeResponse.Role):
                    matchingEmployees = allEmployees.Where(
                        (temp) => (!string.IsNullOrEmpty(temp.Role) ? temp.Role.Contains(
                            searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(EmployeeResponse.IsActive):
                    matchingEmployees = allEmployees.Where(
                        (temp) => (!string.IsNullOrEmpty(temp.IsActive.ToString()) ? temp.IsActive.ToString().Contains(
                            searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                default:
                    matchingEmployees = allEmployees; break;
            }
            return matchingEmployees;

        }

        public async Task<List<EmployeeResponse>> GetSortedEmployees(
            List<EmployeeResponse> allEmployee, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy)) return allEmployee;

            List<EmployeeResponse> sortedList = new List<EmployeeResponse>();

            switch (sortBy)
            {
                case nameof(EmployeeResponse.EmployeeName):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList =allEmployee.OrderBy(
                            temp => temp.EmployeeName, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    else
                    {
                        sortedList = allEmployee.OrderByDescending(
                            temp => temp.EmployeeName, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    break;

                case nameof(EmployeeResponse.Email):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allEmployee.OrderBy(
                            temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    else
                    {
                        sortedList = allEmployee.OrderByDescending(
                            temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    break;
                case nameof(EmployeeResponse.DateOfBirth):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allEmployee.OrderBy(
                            temp => temp.DateOfBirth).ToList();
                    }
                    else
                    {
                        sortedList = allEmployee.OrderByDescending(
                            temp => temp.DateOfBirth).ToList();
                    }
                    break;
                case nameof(EmployeeResponse.Age):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allEmployee.OrderBy(
                            temp => temp.Age).ToList();
                    }
                    else
                    {
                        sortedList = allEmployee.OrderByDescending(
                            temp => temp.Age).ToList();
                    }
                    break;
                case nameof(EmployeeResponse.Gender):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allEmployee.OrderBy(
                            temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    else
                    {
                        sortedList = allEmployee.OrderByDescending(
                            temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    break;
                case nameof(EmployeeResponse.DepartmentName):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allEmployee.OrderBy(
                            temp => temp.DepartmentName, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    else
                    {
                        sortedList = allEmployee.OrderByDescending(
                            temp => temp.DepartmentName, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    break;
                case nameof(EmployeeResponse.Role):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allEmployee.OrderBy(
                            temp => temp.Role, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    else
                    {
                        sortedList = allEmployee.OrderByDescending(
                            temp => temp.Role, StringComparer.OrdinalIgnoreCase).ToList();
                    }
                    break;
                case nameof(EmployeeResponse.IsActive):
                    if (sortOrder == SortOrderOptions.ASC)
                    {
                        sortedList = allEmployee.OrderBy(
                            temp => temp.IsActive).ToList();
                    }
                    else
                    {
                        sortedList = allEmployee.OrderByDescending(
                            temp => temp.IsActive).ToList();
                    }
                    break;

                default:
                    return allEmployee;
            }
            return sortedList;

        }

        public async Task<EmployeeResponse> UpdateEmployees(EmployeeUpdateRequest employeeUpdateRequest)
        {
            if (employeeUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(employeeUpdateRequest));
            }
            ValidationHelper.ModelValidation(employeeUpdateRequest);
            Employee? matchingEmployee = await _dbContext.Employees.FirstOrDefaultAsync(
                (temp) => temp.EmployeeID == employeeUpdateRequest.EmployeeID);
            if (matchingEmployee == null)
            {
                throw new ArgumentException("Given employee doesn't exist");
            }

            //Update the matching employee details
            matchingEmployee.EmployeeName = employeeUpdateRequest.EmployeeName;
            matchingEmployee.Email = employeeUpdateRequest.Email;
            matchingEmployee.DateOfBirth = employeeUpdateRequest.DateOfBirth;
            matchingEmployee.Gender = employeeUpdateRequest.Gender.ToString();
            matchingEmployee.DepartmentID = employeeUpdateRequest.DepartmentID;
            matchingEmployee.Role = employeeUpdateRequest.Role;
            matchingEmployee.IsActive = employeeUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return await ConvertEmployeeToEmployeeResponse(matchingEmployee);
        }

        public async Task<bool> DeleteEmployee(Guid? employeeID)
        {
            if (employeeID == null) throw new ArgumentNullException(nameof(employeeID));

            Employee? employee = await _dbContext.Employees.FirstOrDefaultAsync((temp) => temp.EmployeeID == employeeID);
            if (employee == null)
            {
                return false;
            }
            _dbContext.Employees.Remove( _dbContext.Employees.First(temp => temp.EmployeeID == employeeID));
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
