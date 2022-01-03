using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.ViewComponents {
    public class ShowroomsFilterComponent : ViewComponent {

        private readonly IShowroomService _showroomService;

        private readonly IMapper _mapper;

        public ShowroomsFilterComponent(IShowroomService showroomService, IMapper mapper) {
            _showroomService = showroomService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(uint? selectedShowroom) {

            var model = new SelectList(
                _mapper.Map<IEnumerable<ShowroomDto>, IEnumerable<ShowroomViewModel>>(_showroomService.GetAllShowrooms()), "Id",
                "FullAddress", selectedShowroom);

            return View("ShowroomsFilterComponent", model);
        }
    }
}