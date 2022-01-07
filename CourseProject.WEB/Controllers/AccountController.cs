using System.Security.Claims;
using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CourseProject.WEB.Extensions;
using CourseProject.WEB.Models;

namespace CourseProject.WEB.Controllers {
    public class AccountController : Controller {

        private readonly ISignInService _signInService;

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public AccountController(ISignInService signInService, IUserService userService, IMapper mapper) {
            _signInService = signInService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Register() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) {

            if (!ModelState.IsValid) {
                return View(model);
            }

            var userDto = _mapper.Map<RegisterViewModel, UserDto>(model);

            var result = await _userService.CreateClientAsync(userDto);
            //var result = await _userService.CreateUserAsync(userDto);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model) {

            if (!ModelState.IsValid) {
                return View(model);
            }

            var result = await _signInService.PasswordSignInAsync(model.Email, model.Password, model.RememberMe);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Cabinet() {

            UserViewModel model;

            var clientResult = await _userService.GetClientByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (clientResult.HasErrors) {

                var userResult = await _userService.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (userResult.HasErrors) {
                    TempData["Errors"] = JsonSerializer.Serialize(userResult.Errors);
                    return RedirectToAction(nameof(ErrorController.Error502), "Error");
                }
                model = _mapper.Map<UserDto, UserViewModel>(userResult.Result);
            }
            else {
                model = _mapper.Map<ClientDto, ClientViewModel>(clientResult.Result);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() {
            await _signInService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditPersonalInfo() {

            EditPersonalInfoViewModel model;

            var clientResult = await _userService.GetClientByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (clientResult.HasErrors) {

                var userResult = await _userService.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (userResult.HasErrors) {
                    TempData["Errors"] = JsonSerializer.Serialize(userResult.Errors);
                    return RedirectToAction(nameof(ErrorController.Error502), "Error");
                }

                model = _mapper.Map<UserDto, EditPersonalInfoViewModel>(userResult.Result);
            }
            else {
                model = _mapper.Map<ClientDto, EditPersonalInfoViewModel>(clientResult.Result);
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPersonalInfo(EditPersonalInfoViewModel viewModel) {

            var model = _mapper.Map<EditPersonalInfoViewModel, UserDto>(viewModel);

            var result = await _userService.UpdateUserPersonalDataAsync(model, User);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Cabinet));
        }
    }
}
