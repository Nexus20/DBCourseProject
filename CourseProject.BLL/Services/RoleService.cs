using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CourseProject.BLL.Services; 

public class RoleService : IRoleService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public RoleService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult> CreateRoleAsync(string roleName) {

        var operationResult = new OperationResult();

        var role = await _unitOfWork.RoleManager.FindByNameAsync(roleName);

        if (role != null) {
            operationResult.AddError(nameof(roleName), "Such role already exists");
        }

        role = new IdentityRole(roleName);

        await _unitOfWork.RoleManager.CreateAsync(role);

        return operationResult;
    }
}