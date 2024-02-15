using StaffHub.ServiceContracts.DTO;

namespace StaffHub.ServiceContracts
{
    public interface IDepartmentService
    {
        Task<DepartmentResponse> AddDepartment(DepartmentAddRequest? departmentAddRequest);
        Task<List<DepartmentResponse>> GetAllDepartment();
        Task<DepartmentResponse> GetDepartmentByID(Guid? departmentId);
    }


}