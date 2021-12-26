using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Controllers;
using CourseProject.WEB.Extensions;
using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.Areas.Admin.Controllers {

    [Area("Admin")]
    public class CarsController : Controller {

        private readonly ICarService _carService;

        private readonly IModelService _modelService;

        private readonly IBrandService _brandService;

        private readonly IEquipmentItemCategoryService _equipmentItemCategoryService;

        private readonly IWebHostEnvironment _appEnvironment;

        private readonly IMapper _mapper;

        private readonly IEquipmentItemService _equipmentItemService;

        private readonly IEquipmentItemValueService _equipmentItemValueService;

        public CarsController(ICarService carService, IMapper mapper, IModelService modelService, IWebHostEnvironment appEnvironment, IEquipmentItemCategoryService equipmentItemCategoryService, IEquipmentItemService equipmentItemService, IEquipmentItemValueService equipmentItemValueService, IBrandService brandService) {
            _carService = carService;
            _mapper = mapper;
            _modelService = modelService;
            _appEnvironment = appEnvironment;
            _equipmentItemCategoryService = equipmentItemCategoryService;
            _equipmentItemService = equipmentItemService;
            _equipmentItemValueService = equipmentItemValueService;
            _brandService = brandService;
        }

        // GET: CarsController
        public IActionResult Index([FromQuery] CarFilterViewModel filterModel) {

            GetInfoToCreateFilters();

            var source = filterModel.IsReset ? _carService.GetAllCars() : _carService.GetAllCars(filterModel);

            var model = _mapper.Map<IEnumerable<CarDto>, List<CarViewModel>>(source);

            return View(model);
        }

        // GET: CarsController/Details/5
        [HttpGet]
        [Route("Car/{id:int}")]
        public IActionResult Details(int id) {

            var result = _carService.GetCarById(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<CarDto, CarViewModel>(result.Result);

            return View(model);
        }

        // GET: CarsController/Create
        public IActionResult Create() {

            GetInformationToCreateEditCar();

            return View();
        }

        // POST: CarsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditCarViewModel model) {

            if (!ModelState.IsValid) {
                GetInformationToCreateEditCar();
                return View("Create", model);
            }

            var carDto = _mapper.Map<CreateEditCarViewModel, CarDto>(model);

            var directoryPath = Path.Combine(_appEnvironment.WebRootPath, "img", "cars");
            var result = await _carService.CreateCarAsync(carDto, model.Images, directoryPath);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                GetInformationToCreateEditCar();
                return View("Create", model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateViaFile(IFormFile carFile) {

            var directoryPath = Path.Combine(_appEnvironment.WebRootPath, "carInfoFiles");

            var result = await _carService.CreateCarAsync(carFile, directoryPath);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CarsController/Edit/5
        public IActionResult Edit(int id) {

            var result = _carService.GetCarById(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var editModel = _mapper.Map<CarDto, CreateEditCarViewModel>(result.Result);
            var viewModel = _mapper.Map<CarDto, CarViewModel>(result.Result);

            GetInformationToCreateEditCar();
            ViewBag.EquipmentCategories = _mapper.Map<IEnumerable<EquipmentItemCategoryDto>, IEnumerable<EquipmentItemCategoryViewModel>>(_equipmentItemCategoryService.GetAllCategories());

            return View(new EditCarViewModel() { EditModel = editModel, ViewModel = viewModel });
        }

        // POST: CarsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: CarsController/Delete/5
        public IActionResult Delete(int id) {
            var result = _carService.GetCarById(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<CarDto, CarViewModel>(result.Result);

            return View(model);
        }

        // POST: CarsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id) {

            var directoryPath = Path.Combine(_appEnvironment.WebRootPath, "img", "cars", id.ToString());
            var result = await _carService.DeleteCarAsync(id, directoryPath);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Index));
        }

        private void GetInformationToCreateEditCar() {

            var brands = _modelService.GetAllModels();

            ViewBag.Models = new SelectList(_mapper.Map<IEnumerable<ModelDto>, IEnumerable<ModelViewModel>>(brands), "Id", "NameWithBrand");
        }

        [HttpPost]
        public async Task<IActionResult> AddEquipmentItems(EquipmentItemViewModel model) {

            var dto = _mapper.Map<EquipmentItemViewModel, EquipmentItemDto>(model);

            var result = await _equipmentItemService.CreateItemAsync(dto);

            if (result.HasErrors) {
                return BadRequest();
            }

            return Ok(result.Result);
        }

        public async Task<IActionResult> DeleteEquipmentItem(int equipmentItemId) {

            var result = await _equipmentItemService.DeleteItemAsync(equipmentItemId);

            if (result.HasErrors) {
                return BadRequest();
            }

            return Ok();
        }

        public async Task<IActionResult> DeleteEquipmentItemValue(int equipmentItemValueId) {

            var result = await _equipmentItemValueService.DeleteItemValueAsync(equipmentItemValueId);

            if (result.HasErrors) {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddEquipmentItemValue(EquipmentItemValueViewModel model) {

            var dto = _mapper.Map<EquipmentItemValueViewModel, EquipmentItemValueDto>(model);

            var result = await _equipmentItemValueService.CreateItemValueAsync(dto);

            if (result.HasErrors) {
                return BadRequest();
            }

            return Ok(result.Result);
        }

        private void GetInfoToCreateFilters() {
            ViewBag.Brands = new SelectList(_mapper.Map<IEnumerable<BrandDto>, IEnumerable<BrandViewModel>>(_brandService.GetAllBrands()), "Id", "Name");
            ViewBag.Models = new SelectList(_mapper.Map<IEnumerable<ModelDto>, IEnumerable<ModelViewModel>>(_modelService.GetAllModels()), "Id", "NameWithBrand");
        }
    }
}
