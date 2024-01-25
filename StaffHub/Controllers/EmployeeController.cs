using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using StaffHub.Entities;
using StaffHub.ServiceContracts;
using StaffHub.ServiceContracts.DTO;
using StaffHub.ServiceContracts.Enums;
using System;


namespace StaffHub.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employyeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employyeService = employeeService;
        }
        [Route("/employee/index")]
        [Route("/")] 
        public IActionResult Index(string searchBy, string? searchString, 
            string stortBy=nameof(EmployeeResponse.EmployeeName), 
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

            
            return View(employees);
        }

    }
}

