using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface ISignInService {
    Task<OperationResult> PasswordSignInAsync(string email, string password, bool rememberMe);

    Task<OperationResult> SignOutAsync();
}