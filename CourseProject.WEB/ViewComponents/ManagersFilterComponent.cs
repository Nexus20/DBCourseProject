using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.ViewComponents {
    public class ManagersFilterComponent : ViewComponent {

        private readonly IManagerService _managerService;

        private readonly IMapper _mapper;

        public ManagersFilterComponent(IManagerService managerService, IMapper mapper) {
            _managerService = managerService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(string? selectedManager) {

            var model = new SelectList(
                _mapper.Map<IEnumerable<ManagerDto>, IEnumerable<ManagerViewModel>>(_managerService.GetAllManagers()), "Id",
                "UserId", selectedManager);

            return View("ManagersFilterComponent", model);
        }
    }
}