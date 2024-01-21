using StaffHub.ServiceContracts.DTO;

namespace StaffHub.ServiceContracts
{
    public interface IDepartmentService
    {
        DepartmentResponse AddDepartment(DepartmentAddRequest? departmentAddRequest);
        List<DepartmentResponse> GetAllDepartment();
        DepartmentResponse GetDepartmentByID(Guid? departmentId);
    }


}