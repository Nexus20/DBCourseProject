using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.DAL.Entities;
using CourseProject.WEB.Controllers;
using CourseProject.WEB.Extensions;
using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.WEB.Areas.Admin.Controllers {
    [Area("Admin")]
    public class RolesController : Controller {

        private readonly IUserService _userService;

        private readonly IRoleService _roleService;

        private readonly IMapper _mapper;

        public RolesController(IUserService userService, IRoleService roleService, IMapper mapper) {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

        public IActionResult Index() {

            var model = _mapper.Map<IEnumerable<RoleDto>, List<RoleViewModel>>(_roleService.Roles.ToList());

            return View(model);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name) {

            var result = await _roleService.CreateRoleAsync(name);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                return View("Create", name);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id) {

            var result = await _roleService.DeleteRoleAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult UserList() {

            var model = _mapper.Map<IEnumerable<UserDto>, List<UserViewModel>>(_userService.Users);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId) {

            var userResult = await _userService.GetUserByIdAsync(userId);

            if (userResult.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(userResult.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var userRolesResult = await _userService.GetUserRolesAsync(userId);

            if (userRolesResult.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(userRolesResult.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var allRoles = _roleService.Roles;

            var model = new ChangeRoleViewModel {
                UserId = userResult.Result.Id,
                UserName = userResult.Result.UserName,
                UserRoles = userRolesResult.Result,
                AllRoles = _mapper.Map<IEnumerable<RoleDto>, List<RoleViewModel>>(allRoles)
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles) {

            // Get the user
            var userResult = await _userService.GetUserByIdAsync(userId);

            if (userResult.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(userResult.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var userRolesResult = await _userService.GetUserRolesAsync(userId);

            if (userRolesResult.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(userRolesResult.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            // Get added roles
            var addedRoles = roles.Except(userRolesResult.Result);
            // Get deleted roles
            var removedRoles = userRolesResult.Result.Except(roles);

            var editResult = await _userService.EditUserRolesAsync(userId, addedRoles, removedRoles);

            if (editResult.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(editResult.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction("UserList");
        }
    }
}
