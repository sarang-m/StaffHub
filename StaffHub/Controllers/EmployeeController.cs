using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using StaffHub.Entities;
using StaffHub.ServiceContracts;
using StaffHub.ServiceContracts.DTO;
using StaffHub.ServiceContracts.Enums;
using System;


namespace StaffHub.Controllers
{
    [Route("employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employyeService;
        private readonly IDepartmentService _departmentService;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employyeService = employeeService;
            _departmentService = departmentService;
        }
        [Route("index")]
        [Route("/")] 
        public IActionResult Index(string searchBy, string? searchString, 
            string sortBy=nameof(EmployeeResponse.EmployeeName), 
            SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            //Search functionality
            ViewBag.currentSearchBy = searchBy;
            ViewBag.currentSearchString = searchString;

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {(nameof(EmployeeResponse.EmployeeName)),"Employee Name"},
                {(nameof(EmployeeResponse.Email)),"Email"},
                {(nameof(EmployeeResponse.DateOfBirth)),"DateOfBirth"},
                {(nameof(EmployeeResponse.Gender)),"Gender"},
                {(nameof(EmployeeResponse.DepartmentID)),"Department"},
                {(nameof(EmployeeResponse.Role)),"Role"},
                {(nameof(EmployeeResponse.IsActive)),"Is Active"}
            };

            List<EmployeeResponse> employees = _employyeService.GetFilteredEmployees(searchBy, searchString);
            //Sort functionality
            ViewBag.currentSortBy = sortBy; 
            ViewBag.currentSortOrder = sortOrder.ToString();

            List<EmployeeResponse> sortedEmployees = _employyeService.GetSortedEmployees(
                employees, sortBy, sortOrder);

            return View(sortedEmployees);
        }
        [Route("create")]
        [HttpGet]
        public IActionResult CreateEmployee()
        {
            List<DepartmentResponse> allDepartment =  _departmentService.GetAllDepartment();
            ViewBag.Department = allDepartment;
            return View();
        }

        [Route("create")]
        [HttpPost]
        public IActionResult CreateEmployee(EmployeeAddRequest employeeAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<DepartmentResponse> allDepartment = _departmentService.GetAllDepartment();
                ViewBag.Department = allDepartment;
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(
                    e => e.ErrorMessage).ToList();
                return View();
            }
            EmployeeResponse employeeResponse = _employyeService.AddEmployee(employeeAddRequest);
            return RedirectToAction("index","Employee");
        }

    }
}

