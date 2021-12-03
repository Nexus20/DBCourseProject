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

    public IEnumerable<UserDto> Users =>
        _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(_unitOfWork.UserManager.Users);

    public async Task<OperationResult<UserDto>> GetUserByIdAsync(string userId) {

        var operationResult = new OperationResult<UserDto>();

        var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

        if (user == null) {
            operationResult.AddError(nameof(userId), "Such user not found");
            return operationResult;
        }

        operationResult.Result = _mapper.Map<User, UserDto>(user);

        return operationResult;
    }

    public async Task<OperationResult<IList<string>>> GetUserRolesAsync(string userId) {

        var operationResult = new OperationResult<IList<string>>();

        var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

        if (user == null) {
            operationResult.AddError(nameof(userId), "Such user not found");
            return operationResult;
        }

        operationResult.Result = await _unitOfWork.UserManager.GetRolesAsync(user);

        return operationResult;
    }

    public async Task<OperationResult> EditUserRolesAsync(string userId, IEnumerable<string> addedRoles, IEnumerable<string> removedRoles) {

        var operationResult = new OperationResult();

        var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

        if (user == null) {
            operationResult.AddError(nameof(userId), "Such user not found");
            return operationResult;
        }

        var result = await _unitOfWork.UserManager.AddToRolesAsync(user, addedRoles);

        if (result.Errors.Any()) {
            foreach (var error in result.Errors) {
                operationResult.AddError(error.Code, error.Description);
            }

            return operationResult;
        }

        result = await _unitOfWork.UserManager.RemoveFromRolesAsync(user, removedRoles);

        if (result.Errors.Any()) {
            foreach (var error in result.Errors) {
                operationResult.AddError(error.Code, error.Description);
            }

            return operationResult;
        }

        return operationResult;
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