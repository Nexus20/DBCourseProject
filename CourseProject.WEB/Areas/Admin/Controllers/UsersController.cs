using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Controllers;
using CourseProject.WEB.Extensions;
using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.WEB.Areas.Admin.Controllers {

    [Area("Admin")]
    public class UsersController : Controller {

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper) {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index() {

            var model = _mapper.Map<IEnumerable<UserDto>, List<UserViewModel>>(_userService.Users);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model) {

            if (!ModelState.IsValid) {
                return View(model);
            }

            var dto = _mapper.Map<CreateUserViewModel, UserDto>(model);

            var result = await _userService.CreateUserAsync(dto);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id) {

            var result = await _userService.GetUserByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<UserDto, EditUserViewModel>(result.Result);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model) {

            if (!ModelState.IsValid) {
                return View(model);
            }

            var dto = _mapper.Map<EditUserViewModel, UserDto>(model);

            var result = await _userService.EditUserAsync(dto);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id) {

            var result = await _userService.DeleteUserAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id) {

            var result = await _userService.GetUserByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<UserDto, ChangePasswordViewModel>(result.Result);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model) {

            if (!ModelState.IsValid) {
                return View(model);
            }

            var dto = _mapper.Map<ChangePasswordViewModel, UserDto>(model);

            var result = await _userService.ChangeUserPasswordAsync(dto);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
