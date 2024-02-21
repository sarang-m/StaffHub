using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Rotativa.AspNetCore;
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
        public async Task<IActionResult> Index(string searchBy, string? searchString, 
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


            List<EmployeeResponse> employees = await _employyeService.GetFilteredEmployees(searchBy, searchString);
            //Sort functionality
            ViewBag.currentSortBy = sortBy; 
            ViewBag.currentSortOrder = sortOrder.ToString();

            List<EmployeeResponse> sortedEmployees =await _employyeService.GetSortedEmployees(
                employees, sortBy, sortOrder);

            return View(sortedEmployees);
        }
        [Route("create")]
        [HttpGet]
        public async Task<IActionResult> CreateEmployee()
        {
            List<DepartmentResponse> allDepartment = await _departmentService.GetAllDepartment();
            ViewBag.Department = allDepartment.Select( 
                temp => new SelectListItem() { 
                    Text = temp.DepartmentName, 
                    Value = temp.DeparmentId.ToString()});

            return View();
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeAddRequest employeeAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<DepartmentResponse> allDepartment = await _departmentService.GetAllDepartment();
                ViewBag.Department = allDepartment.Select(
                temp => new SelectListItem()
                {
                    Text = temp.DepartmentName,
                    Value = temp.DeparmentId.ToString()
                });
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(
                    e => e.ErrorMessage).ToList();
                return View();
            }
            EmployeeResponse employeeResponse = await _employyeService.AddEmployee(employeeAddRequest);
            return RedirectToAction("index","Employee");
        }
        [HttpGet]
        [Route("edit/{employeeID}")]
        public async Task<IActionResult> Edit(Guid employeeID)
        {
            EmployeeResponse? employeeResponse =await _employyeService.GetEmployeeByID(employeeID);
            if (employeeResponse == null)
            {
                return RedirectToAction("index");
            }

            List<DepartmentResponse> allDepartment =await _departmentService.GetAllDepartment();
            ViewBag.Department = allDepartment.Select(
                temp => new SelectListItem()
                {
                    Text = temp.DepartmentName,
                    Value = temp.DeparmentId.ToString()
                });

            EmployeeUpdateRequest employeeUpdaterequest = employeeResponse.ToEmployeeUpdateRequest();
            return View(employeeUpdaterequest);

        }
        [HttpPost]
        [Route("edit/{employeeID}")]
        public async Task<IActionResult> Edit(EmployeeUpdateRequest employeeUpdateRequest)
        {
            EmployeeResponse employeeResponse = await _employyeService.GetEmployeeByID(employeeUpdateRequest.EmployeeID);

            if (employeeResponse == null)
            {
                return RedirectToAction("index");
            }
            if (ModelState.IsValid)
            {
                EmployeeResponse updatedEmployee = await _employyeService.UpdateEmployees(employeeUpdateRequest);
                return RedirectToAction("index");
            }
            else
            {
                List<DepartmentResponse> allDepartment = await _departmentService.GetAllDepartment();
                ViewBag.Department = allDepartment.Select(
                temp => new SelectListItem()
                {
                    Text = temp.DepartmentName,
                    Value = temp.DeparmentId.ToString()
                });
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(
                    e => e.ErrorMessage).ToList();
                return View(employeeResponse.ToEmployeeUpdateRequest());
            }
            
        }
        [HttpGet]
        [Route("delete/{employeeID}")]
        public async Task<IActionResult> Delete(Guid employeeId)
        {
            EmployeeResponse? employeeResponse = await _employyeService.GetEmployeeByID(employeeId);
            if (employeeResponse != null)
            {
                return View(employeeResponse);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        [Route("delete/{employeeID}")]
        public async Task<IActionResult> Delete(EmployeeResponse employeeResponse)
        {
            EmployeeResponse? employee = await _employyeService.GetEmployeeByID(employeeResponse.EmployeeID);
            if (employee != null)
            {
                bool isDeleated = await _employyeService.DeleteEmployee(employee.EmployeeID);
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }
        [Route("employeePDF")]
        public async Task<IActionResult> EmployeePDF()
        {
            List<EmployeeResponse> employees = await _employyeService.GetAllEmployees();
            return new ViewAsPdf("EmployeePDF", employees, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins()
                {
                    Top = 20,
                    Right = 20,
                    Bottom = 20,
                    Left = 20
                },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
            };
        }

    }
}

