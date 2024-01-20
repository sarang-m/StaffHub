using StaffHub.Entities;
using StaffHub.ServiceContracts;
using StaffHub.ServiceContracts.DTO;

namespace StaffHub.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly List<Department> _department;
        public DepartmentService()
        {
            _department = new List<Department>();
        }
        public DepartmentResponse AddDepartment(DepartmentAddRequest? departmentAddRequest)
        {
            if (departmentAddRequest == null)
            {
                throw new ArgumentNullException(nameof(departmentAddRequest));
            }
            if (departmentAddRequest.DepartmentName == null)
            {
                throw new ArgumentException(nameof(departmentAddRequest.DepartmentName));
            }
            if (_department.Where(
                (depatment) => depatment.DepartmenName == departmentAddRequest.DepartmentName).Count()>0)
            {
                throw new ArgumentException("Department already exist");
            }
            Department department = departmentAddRequest.ToDepartment();
            department.DepartmentId = Guid.NewGuid();
            _department.Add(department);

            return department.ToDepartmentResponse();
        }
    }
}