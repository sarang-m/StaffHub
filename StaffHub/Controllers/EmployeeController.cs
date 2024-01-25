using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using StaffHub.Entities;
using StaffHub.ServiceContracts;
using StaffHub.ServiceContracts.DTO;
using System;
using System.Reflection;

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
        public IActionResult Index()
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {(nameof(EmployeeResponse.EmployeeName)),"Employee Name"},
                {(nameof(EmployeeResponse.Email)),"Email"},
                {(nameof(EmployeeResponse.DateOfBirth)),"DateOfBirth"},
                {(nameof(EmployeeResponse.Gender)),"Gender"},
                {(nameof(EmployeeResponse.DepartmentID)),"Department"},
                {(nameof(EmployeeResponse.Role)),"Role"},
            };

            List<EmployeeResponse> employee = _employyeService.GetAllEmployees();
            return View(employee);
        }
    }
}

