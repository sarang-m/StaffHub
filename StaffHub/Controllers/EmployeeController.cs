using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewBag.Department = allDepartment.Select( 
                temp => new SelectListItem() { 
                    Text = temp.DepartmentName, 
                    Value = temp.DeparmentId.ToString()});

            return View();
        }

        [Route("create")]
        [HttpPost]
        public IActionResult CreateEmployee(EmployeeAddRequest employeeAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<DepartmentResponse> allDepartment = _departmentService.GetAllDepartment();
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
            EmployeeResponse employeeResponse = _employyeService.AddEmployee(employeeAddRequest);
            return RedirectToAction("index","Employee");
        }
        [HttpGet]
        [Route("edit/{employeeID}")]
        public IActionResult Edit(Guid employeeID)
        {
            EmployeeResponse? employeeResponse = _employyeService.GetEmployeeByID(employeeID);
            if (employeeResponse == null)
            {
                return RedirectToAction("index");
            }

            List<DepartmentResponse> allDepartment = _departmentService.GetAllDepartment();
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
        public IActionResult Edit(EmployeeUpdateRequest employeeUpdateRequest)
        {
            EmployeeResponse employeeResponse =  _employyeService.GetEmployeeByID(employeeUpdateRequest.EmployeeID);

            if (employeeResponse == null)
            {
                return RedirectToAction("index");
            }
            if (ModelState.IsValid)
            {
                EmployeeResponse updatedEmployee =_employyeService.UpdateEmployees(employeeUpdateRequest);
                return RedirectToAction("index");
            }
            else
            {
                List<DepartmentResponse> allDepartment = _departmentService.GetAllDepartment();
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
        public IActionResult Delete(Guid employeeId)
        {
            EmployeeResponse? employeeResponse = _employyeService.GetEmployeeByID(employeeId);
            if (employeeResponse != null)
            {
                return View(employeeResponse);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        [Route("delete/{employeeID}")]
        public IActionResult Delete(EmployeeResponse employeeResponse)
        {
            EmployeeResponse? employee = _employyeService.GetEmployeeByID(employeeResponse.EmployeeID);
            if (employee != null)
            {
                bool isDeleated = _employyeService.DeleteEmployee(employee.EmployeeID);
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }

    }
}

