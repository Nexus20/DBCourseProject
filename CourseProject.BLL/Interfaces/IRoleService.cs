using CourseProject.BLL.DTO;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IRoleService {
    Task<OperationResult> CreateRoleAsync(string roleName);

    IEnumerable<RoleDto> Roles { get; }

    Task<OperationResult> DeleteRoleAsync(string id);
}