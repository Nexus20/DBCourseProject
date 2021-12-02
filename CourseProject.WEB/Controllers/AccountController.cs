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

        private readonly IRoleService _roleService;

        private readonly IMapper _mapper;

        public AccountController(ISignInService signInService, IUserService userService, IMapper mapper, IRoleService roleService) {
            _signInService = signInService;
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
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

            var result = await _userService.CreateUserAsync(userDto);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() {
            await _signInService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
