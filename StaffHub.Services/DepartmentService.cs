using Microsoft.EntityFrameworkCore;
using StaffHub.Entities;
using StaffHub.ServiceContracts;
using StaffHub.ServiceContracts.DTO;

namespace StaffHub.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EmployeesDbContext _dbContext;
        public DepartmentService(EmployeesDbContext employeesDbContext)
        {
            _dbContext = employeesDbContext;

        }
        public async Task<DepartmentResponse> AddDepartment(DepartmentAddRequest? departmentAddRequest)
        {
            if (departmentAddRequest == null)
            {
                throw new ArgumentNullException(nameof(departmentAddRequest));
            }
            if (departmentAddRequest.DepartmentName == null)
            {
                throw new ArgumentException(nameof(departmentAddRequest.DepartmentName));
            }
            if (await _dbContext.Department.CountAsync(
                (depatment) => depatment.DepartmenName == departmentAddRequest.DepartmentName) >0)
            {
                throw new ArgumentException("Department already exist");
            }
            Department department = departmentAddRequest.ToDepartment();
            department.DepartmentId = Guid.NewGuid();
            _dbContext.Department.Add(department);
            await _dbContext.SaveChangesAsync();

            return department.ToDepartmentResponse();
        }

        public async Task<List<DepartmentResponse>> GetAllDepartment()
        {
            List<DepartmentResponse> depdepartmentResponse = await _dbContext.Department.Select(
                (department) => department.ToDepartmentResponse()).ToListAsync();
            return depdepartmentResponse;
        }

        public async Task<DepartmentResponse?> GetDepartmentByID(Guid? departmentId)
        {
            if (departmentId == null)
            {
                return null;
            }
            Department? department =  await _dbContext.Department.FirstOrDefaultAsync(
                department => department.DepartmentId == departmentId);
            if (department == null)
            {
                return null;
            }
            return department.ToDepartmentResponse(); 
        }
    }
}