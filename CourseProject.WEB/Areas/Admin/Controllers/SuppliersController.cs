using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Controllers;
using CourseProject.WEB.Extensions;
using CourseProject.WEB.Models;
using CourseProject.WEB.Models.FilterViewModels;
using CourseProject.WEB.Models.PaginatedFilteredViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.Areas.Admin.Controllers {
    [Area("Admin")]
    public class SuppliersController : Controller {

        private readonly IBrandService _brandService;

        private readonly ISupplierService _supplierService;

        private readonly IMapper _mapper;

        public SuppliersController(IBrandService brandService, ISupplierService supplierService, IMapper mapper) {
            _brandService = brandService;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/suppliers")]
        public async Task<IActionResult> Index([FromQuery] SupplierFilterViewModel filterModel) {

            var source = await _supplierService.GetAllSuppliersAsync(_mapper.Map<SupplierFilterViewModel, SupplierFilterModel>(filterModel));

            var model = new SuppliersWithFiltersViewModel() {
                Suppliers = _mapper.Map<IEnumerable<SupplierDto>, List<SupplierViewModel>>(source.Dtos),
                Filters = new SupplierFilterViewModel(),
                PageViewModel = new PageViewModel(source.PossibleDtosCount, filterModel.PageNumber, filterModel.TakeCount)
            };

            if (ModelState.IsValid) {
                model.SelectedBrand = filterModel.BrandId;
                model.SelectedOrderType = filterModel.OrderType;
                model.Name = filterModel.Name;
                model.Phone = filterModel.Phone;
                model.Email = filterModel.Email;
            }

            return View(model);
        }

        // GET: ModelsController/Details/5
        [HttpGet]
        [Route("/suppliers/{id:int}")]
        public async Task<IActionResult> Details(int id) {

            var result = await _supplierService.GetSupplierByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<SupplierDto, SupplierViewModel>(result.Result);

            return View(model);
        }

        // GET: ModelsController/Create
        [HttpGet]
        [Route("/suppliers/new")]
        public IActionResult Create() {
            GetInformationToCreateEditSupplier();
            return View();
        }

        // POST: SuppliersController/Create
        [HttpPost]
        [Route("/suppliers/new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditSupplierViewModel model) {

            if (!ModelState.IsValid) {
                GetInformationToCreateEditSupplier();
                return View("Create", model);
            }

            var dto = _mapper.Map<CreateEditSupplierViewModel, SupplierDto>(model);

            var result = await _supplierService.CreateSupplierAsync(dto);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                GetInformationToCreateEditSupplier();
                return View("Create", model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ModelsController/Edit/5
        public async Task<IActionResult> Edit(int id) {

            var result = await _supplierService.GetSupplierByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            GetInformationToCreateEditSupplier();
            var model = _mapper.Map<SupplierDto, CreateEditSupplierViewModel>(result.Result);

            return View(model);
        }

        // POST: ModelsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEditSupplierViewModel model) {

            if (!ModelState.IsValid) {
                GetInformationToCreateEditSupplier();
                return View("Edit", model);
            }

            var modelDto = _mapper.Map<CreateEditSupplierViewModel, SupplierDto>(model);

            var result = await _supplierService.EditSupplierAsync(modelDto);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                GetInformationToCreateEditSupplier();
                return View("Edit", model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ModelsController/Delete/5
        public async Task<IActionResult> Delete(int id) {

            var result = await _supplierService.GetSupplierByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<SupplierDto, SupplierViewModel>(result.Result);

            return View(model);
        }

        // POST: ModelsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id) {

            var result = await _supplierService.DeleteSupplierAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Index));
        }

        private void GetInformationToCreateEditSupplier() {

            var brands = _brandService.GetAllBrands();

            ViewBag.Brands = new SelectList(_mapper.Map<IEnumerable<BrandDto>, IEnumerable<BrandViewModel>>(brands), "Id", "Name");
        }

    }
}
