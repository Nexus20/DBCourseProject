using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services; 

public class UserService : IUserService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult> CreateUserAsync(UserDto userDto) {

        var operationResult = new OperationResult();

        var user = await _unitOfWork.UserManager.FindByEmailAsync(userDto.Email);

        if (user != null) {
            operationResult.AddError(nameof(userDto.Email), "User with such email already exists");
            return operationResult;
        }

        user = _mapper.Map<UserDto, User>(userDto);
        user.Name = user.Surname = user.Patronymic = "";

        var result = await _unitOfWork.UserManager.CreateAsync(user, userDto.Password);

        if (result.Errors.Any()) {

            foreach (var error in result.Errors) {
                operationResult.AddError(error.Code, error.Description);
            }

            return operationResult;
        }

        await _unitOfWork.UserManager.AddToRoleAsync(user, userDto.Role);
        await _unitOfWork.SignInManager.SignInAsync(user, false);

        return operationResult;
    }
}