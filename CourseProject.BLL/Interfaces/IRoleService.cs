using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IRoleService {
    public Task<OperationResult> CreateRoleAsync(string roleName);
}