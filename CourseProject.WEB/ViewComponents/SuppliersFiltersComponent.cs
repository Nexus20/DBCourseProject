using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.ViewComponents {
    public class SuppliersFiltersComponent : ViewComponent {

        private readonly ISupplierService _supplierService;

        private readonly IMapper _mapper;

        public SuppliersFiltersComponent(ISupplierService supplierService, IMapper mapper) {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(uint? selectedSupplier) {

            var model = new SelectList(
                _mapper.Map<IEnumerable<SupplierDto>, IEnumerable<SupplierViewModel>>(_supplierService.GetAllSuppliers()), "Id",
                "Name", selectedSupplier);

            return View("SuppliersListComponent", model);
        }
    }
}