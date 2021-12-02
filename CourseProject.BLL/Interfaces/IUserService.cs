using CourseProject.BLL.DTO;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IUserService {
    Task<OperationResult> CreateUserAsync(UserDto userDto);
}