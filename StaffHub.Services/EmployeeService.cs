using StaffHub.Entities;
using StaffHub.ServiceContracts;
using StaffHub.ServiceContracts.DTO;
using StaffHub.ServiceContracts.Enums;
using StaffHub.Services.Helpers;


namespace StaffHub.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees;
        private readonly IDepartmentService _departmentService;

        public EmployeeService(bool initialize = true)
        {
            _employees = new List<Employee>();
            _departmentService = new DepartmentService();
            if (initialize)
            {
                _employees.Add(new Employee() 
                { 
                    EmployeeID = Guid.Parse("2535278F-15DD-4DFD-9C16-C4C5002EA7FF"),
                    EmployeeName = "Aurilia Stuchbury",
                    Email = "astuchbury0@redcross.org",
                    DateOfBirth = DateTime.Parse("1998-05-13"),
                    Gender = "Female",
                    Role = "Subcontractor",
                    IsActive = false,
                    DepartmentID = Guid.Parse("50353625-6473-47DB-8118-1E301C816DEC")
                });

                _employees.Add(new Employee()
                {
                    EmployeeID = Guid.Parse("F736255F-D604-45BE-8312-5D66E6C263E1"),
                    EmployeeName = "Gal Dahill",
                    Email = "gdahill1@printfriendly.com",
                    DateOfBirth = DateTime.Parse("1997-10-16"),
                    Gender = "Male",
                    Role = "Subcontractor",
                    IsActive = false,
                    DepartmentID = Guid.Parse("50353625-6473-47DB-8118-1E301C816DEC")
                });
                _employees.Add(new Employee()
                {
                    EmployeeID = Guid.Parse("5FC9EE96-0E05-4B3A-9BAA-B4C0C30DE70C"),
                    EmployeeName = "Bobbie Costain",
                    Email = "bcostain2@ftc.gov",
                    DateOfBirth = DateTime.Parse("1997-10-16"),
                    Gender = "Female",
                    Role = "Surveyor",
                    IsActive = true,
                    DepartmentID = Guid.Parse("1213D293-587F-4DD9-A66F-70A7DC033E7A")
                });
                _employees.Add(new Employee()
                {
                    EmployeeID = Guid.Parse("E37D2865-8E15-4AE6-A468-40518827FC6E"),
                    EmployeeName = "Boony Handman",
                    Email = "bhandman3@scientificamerican.com",
                    DateOfBirth = DateTime.Parse("1995-02-20"),
                    Gender = "Male",
                    Role = "Subcontractor",
                    IsActive = true,
                    DepartmentID = Guid.Parse("484BF435-BBEE-4B9B-8E56-BE32ED8F47CB")
                });
                _employees.Add(new Employee()
                {
                    EmployeeID = Guid.Parse("3BB36694-02C3-4FDD-ABD6-380241D757AF"),
                    EmployeeName = "Hasheem Rolin",
                    Email = "hrolin4@samsung.com",
                    DateOfBirth = DateTime.Parse("1995-03-22"),
                    Gender = "Male",
                    Role = "Architect",
                    IsActive = true,
                    DepartmentID = Guid.Parse("ECE3E74F-2847-4CB2-B0EA-9E8DEA297787")
                });
                _employees.Add(new Employee()
                {
                    EmployeeID = Guid.Parse("CB611A93-8423-4728-A692-D3AB58AEA80C"),
                    EmployeeName = "Andriana Cescot",
                    Email = "acescot5@yale.edu",
                    DateOfBirth = DateTime.Parse("1993-03-03"),
                    Gender = "Female",
                    Role = "Architect",
                    IsActive = true,
                    DepartmentID = Guid.Parse("ECE3E74F-2847-4CB2-B0EA-9E8DEA297787")
                });

            }
        }
        private EmployeeResponse ConvertEmployeeToEmployeeResponse(Employee employee)
        {
            EmployeeResponse employeeResponse = employee.ToEmployeeResponse();
            DepartmentResponse department = _departmentService.GetDepartmentByID(employee.DepartmentID);

            if (department != null) employeeResponse.DepartmentName = department.DepartmentName;
            else employeeResponse.DepartmentName = null;
            return employeeResponse;

        }
        public EmployeeResponse AddEmployee(EmployeeAddRequest? employeeAddRequest)
        {
            if (employeeAddRequest == null) throw new ArgumentNullException(nameof(employeeAddRequest));

            //Model validation
            ValidationHelper.ModelValidation(employeeAddRequest);

            Employee employee = employeeAddRequest.ToEmployee();
            employee.EmployeeID = Guid.NewGuid();
            _employees.Add(employee);

            return ConvertEmployeeToEmployeeResponse(employee);
        }
            
        public List<EmployeeResponse> GetAllEmployees()
        {
            List<EmployeeResponse> employeeResponses = _employees.Select((temp) => temp.ToEmployeeResponse()).ToList();
            return employeeResponses;
        }

        public EmployeeResponse GetEmployeeByID(Guid? employeeID)
        {
            if (employeeID == null) return null;

            Employee? employee = _employees.FirstOrDefault((temp) => temp.EmployeeID == employeeID);
            if (employee == null) return null;

            return employee.ToEmployeeResponse();
        }

        public List<EmployeeResponse> GetFilteredEmployees(string searchBy, string? searchString)
        {
            List<EmployeeResponse> allEmployees= GetAllEmployees();
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

        public List<EmployeeResponse> GetSortedEmployees(
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

        public EmployeeResponse UpdateEmployees(EmployeeUpdateRequest employeeUpdateRequest)
        {
            if (employeeUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(employeeUpdateRequest));
            }
            ValidationHelper.ModelValidation(employeeUpdateRequest);
            Employee? matchingEmployee = _employees.FirstOrDefault(
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

            return matchingEmployee.ToEmployeeResponse();
        }

        public bool DeleteEmployee(Guid? employeeID)
        {
            if (employeeID == null) throw new ArgumentNullException(nameof(employeeID));

            Employee? employee = _employees.FirstOrDefault((temp) => temp.EmployeeID == employeeID);
            if (employee == null)
            {
                return false;
            }
            _employees.RemoveAll((temp) => temp.EmployeeID == employeeID);
            return true;
        }
    }
}
