using CourseProject.BLL.DTO;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces;

public interface IUserService {
    Task<OperationResult> CreateUserAsync(UserDto userDto);

    IEnumerable<UserDto> Users { get; }

    Task<OperationResult<UserDto>> GetUserByIdAsync(string userId);

    Task<OperationResult<IList<string>>> GetUserRolesAsync(string userId);

    Task<OperationResult> EditUserRolesAsync(string userId, IEnumerable<string> addedRoles,
        IEnumerable<string> removedRoles);

    Task<OperationResult> EditUserAsync(UserDto dto);

    Task<OperationResult> DeleteUserAsync(string userId);

    Task<OperationResult> ChangeUserPasswordAsync(UserDto dto);

    Task<OperationResult<ClientDto>> GetClientByIdAsync(string userId);

    Task<OperationResult> CreateClientAsync(UserDto userDto);
}