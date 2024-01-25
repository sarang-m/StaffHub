using StaffHub.Entities;
using StaffHub.ServiceContracts;
using StaffHub.ServiceContracts.DTO;

namespace StaffHub.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly List<Department> _department;
        public DepartmentService(bool initialize = true)
        {
            _department = new List<Department>();
            if (initialize)
            {
                _department.AddRange(new List<Department>() 
                { 
                    new Department() 
                    { 
                        DepartmentId = Guid.Parse("50353625-6473-47DB-8118-1E301C816DEC"),
                        DepartmenName = "IT" 
                    },
                    new Department()
                    {
                        DepartmentId = Guid.Parse("1213D293-587F-4DD9-A66F-70A7DC033E7A"),
                        DepartmenName = "Sales"
                    },
                    new Department()
                    {
                        DepartmentId = Guid.Parse("484BF435-BBEE-4B9B-8E56-BE32ED8F47CB"),
                        DepartmenName = "Accounting"
                    },
                    new Department()
                    {
                        DepartmentId = Guid.Parse("ECE3E74F-2847-4CB2-B0EA-9E8DEA297787"),
                        DepartmenName = "HR"
                    },
                });

            //50353625-6473-47DB-8118-1E301C816DEC
            //1213D293-587F-4DD9-A66F-70A7DC033E7A
            //484BF435-BBEE-4B9B-8E56-BE32ED8F47CB
            //ECE3E74F-2847-4CB2-B0EA-9E8DEA297787

        }
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

        public List<DepartmentResponse> GetAllDepartment()
        {
            List<DepartmentResponse> depdepartmentResponse = _department.Select(
                (department) => department.ToDepartmentResponse()).ToList();
            return depdepartmentResponse;
        }

        public DepartmentResponse GetDepartmentByID(Guid? departmentId)
        {
            if (departmentId == null)
            {
                return null;
            }
            Department? department =  _department.FirstOrDefault(
                department => department.DepartmentId == departmentId);
            if (department == null)
            {
                return null;
            }
            return department.ToDepartmentResponse(); 
        }
    }
}