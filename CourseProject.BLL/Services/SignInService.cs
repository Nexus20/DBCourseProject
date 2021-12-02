using AutoMapper;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services; 

public class SignInService : ISignInService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public SignInService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult> PasswordSignInAsync(string email, string password, bool rememberMe) {

        var operationResult = new OperationResult();

        var result = await _unitOfWork.SignInManager.PasswordSignInAsync(email, password, rememberMe, false);

        if (!result.Succeeded) {
            operationResult.AddError("Login credentials", "Invalid login or(and) password");
        }

        return operationResult;
    }
}