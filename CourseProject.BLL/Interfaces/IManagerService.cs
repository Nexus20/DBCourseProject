using System.Security.Claims;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IManagerService {
    Task<OperationResult> CreateManagerAsync(ManagerDto managerDto);

    IEnumerable<ManagerDto> GetAllManagers();

    Task<OperationResult<ManagerDto>> GetManagerByClaimsAsync(ClaimsPrincipal claims);
}