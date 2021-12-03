using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Controllers;
using CourseProject.WEB.Extensions;
using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.Areas.Admin.Controllers {

    [Area("Admin")]
    public class ManagersController : Controller {

        private readonly IManagerService _managerService;

        private readonly IShowroomService _showroomService;

        private readonly IMapper _mapper;

        public ManagersController(IManagerService managerService, IMapper mapper, IShowroomService showroomService) {
            _managerService = managerService;
            _mapper = mapper;
            _showroomService = showroomService;
        }

        public IActionResult Index() {

            var model = _mapper.Map<IEnumerable<ManagerDto>, List<ManagerViewModel>>(_managerService.GetAllManagers());

            return View(model);
        }

        [HttpGet]
        public IActionResult Create() {
            GetInformationToCreateEditManager();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateManagerViewModel model) {

            if (!ModelState.IsValid) {
                GetInformationToCreateEditManager();
                return View(model);
            }

            var dto = new ManagerDto() {
                User = new UserDto() {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password
                },
                ShowroomId = model.ShowroomId
            };

            var result = await _managerService.CreateManagerAsync(dto);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                GetInformationToCreateEditManager();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        private void GetInformationToCreateEditManager() {

            var showrooms = _showroomService.GetAllShowrooms();

            ViewBag.Showrooms = new SelectList(_mapper.Map<IEnumerable<ShowroomDto>, IEnumerable<ShowroomViewModel>>(showrooms), "Id", "FullAddress");
        }
    }
}
