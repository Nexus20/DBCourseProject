using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.ViewComponents {
    public class BrandsFilterComponent : ViewComponent {

        private readonly IBrandService _brandService;

        private readonly IMapper _mapper;

        public BrandsFilterComponent(IBrandService brandService, IMapper mapper) {
            _brandService = brandService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(uint? selectedBrand) {

            var model = new SelectList(
                _mapper.Map<IEnumerable<BrandDto>, IEnumerable<BrandViewModel>>(_brandService.GetAllBrands()), "Id",
                "Name", selectedBrand);

            return View("BrandsFilterComponent", model);
        }
    }
}