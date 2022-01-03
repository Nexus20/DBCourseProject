using System.Security.Claims;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services; 

public class ManagerService : IManagerService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IServiceProvider _services;

    public ManagerService(IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider services) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _services = services;
    }

    public IEnumerable<ManagerDto> GetAllManagers() {

        var source = _unitOfWork.GetRepository<IRepository<Manager>, Manager>()
            .Find(null, null, null, null, m => m.User, m => m.Showroom);

        return _mapper.Map<IEnumerable<Manager>, IEnumerable<ManagerDto>>(source);
    }

    public async Task<OperationResult<ManagerDto>> GetManagerByClaimsAsync(ClaimsPrincipal claims) {

        var result = new OperationResult<ManagerDto>();

        var userId = _unitOfWork.UserManager.GetUserId(claims);

        if (string.IsNullOrWhiteSpace(userId)) {
            result.AddError("claims", "User with claims not found");
            return result;
        }

        var manager = await _unitOfWork.GetRepository<IRepository<Manager>, Manager>()
            .FirstOrDefaultAsync(m => m.UserId == userId);

        if (manager == null) {
            result.AddError("userId", "Manager with such user id not found");
            return result;
        }

        result.Result = _mapper.Map<Manager, ManagerDto>(manager);
        return result;
    }

    public async Task<OperationResult> CreateManagerAsync(ManagerDto managerDto) {

        var operationResult = new OperationResult();

        await using var transaction = _unitOfWork.BeginTransaction();

        try {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(managerDto.User.Email);

            if (user != null) {
                operationResult.AddError(nameof(managerDto.User.Email), "User with such email already exists");
                await transaction.RollbackAsync();
                return operationResult;
            }

            user = _mapper.Map<UserDto, User>(managerDto.User);
            user.Name = user.Surname = user.Patronymic = "";

            var result = await _unitOfWork.UserManager.CreateAsync(user, managerDto.User.Password);

            if (result.Errors.Any()) {

                foreach (var error in result.Errors) {
                    operationResult.AddError(error.Code, error.Description);
                }
                await transaction.RollbackAsync();
                return operationResult;
            }

            await _unitOfWork.UserManager.AddToRoleAsync(user, "user");
            await _unitOfWork.UserManager.AddToRoleAsync(user, "manager");

            var showroom = await _unitOfWork.GetRepository<IRepository<Showroom>, Showroom>()
                .FirstOrDefaultAsync(s => s.Id == managerDto.ShowroomId);

            if (showroom == null) {
                operationResult.AddError(nameof(managerDto.ShowroomId), "There is no such showroom");
                await transaction.RollbackAsync();
                return operationResult;
            }

            var manager = new Manager() {
                ShowroomId = showroom.Id,
                UserId = user.Id
            };

            await _unitOfWork.GetRepository<IRepository<Manager>, Manager>().CreateAsync(manager);
            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception ex) {
            await transaction.RollbackAsync();
            operationResult.AddError("Unexpected", "There is an unexpected error");
        }

        return operationResult;
    }
}