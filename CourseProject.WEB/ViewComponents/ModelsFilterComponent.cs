using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.ViewComponents {
    public class ModelsFilterComponent : ViewComponent {

        private readonly IModelService _modelService;

        private readonly IMapper _mapper;

        public ModelsFilterComponent(IModelService modelService, IMapper mapper) {
            _modelService = modelService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(uint? selectedModel) {

            var model = new SelectList(
                _mapper.Map<IEnumerable<ModelDto>, IEnumerable<ModelViewModel>>(_modelService.GetAllModels()), "Id",
                "Name", selectedModel);

            return View("ModelsFilterComponent", model);
        }
    }
}