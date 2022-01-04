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
    public class CarsController : Controller {

        private readonly ICarService _carService;

        private readonly IModelService _modelService;

        private readonly IEquipmentItemCategoryService _equipmentItemCategoryService;

        private readonly IWebHostEnvironment _appEnvironment;

        private readonly IMapper _mapper;

        private readonly IEquipmentItemService _equipmentItemService;

        private readonly IEquipmentItemValueService _equipmentItemValueService;

        public CarsController(ICarService carService, IMapper mapper, IModelService modelService, IWebHostEnvironment appEnvironment, IEquipmentItemCategoryService equipmentItemCategoryService, IEquipmentItemService equipmentItemService, IEquipmentItemValueService equipmentItemValueService) {
            _carService = carService;
            _mapper = mapper;
            _modelService = modelService;
            _appEnvironment = appEnvironment;
            _equipmentItemCategoryService = equipmentItemCategoryService;
            _equipmentItemService = equipmentItemService;
            _equipmentItemValueService = equipmentItemValueService;
        }

        // GET: CarsController
        public async Task<IActionResult> Index([FromQuery] CarFilterViewModel filterModel) {

            var source = await _carService.GetAllCarsAsync(_mapper.Map<CarFilterViewModel, CarFilterModel>(filterModel));

            var model = new CarsWithFiltersViewModel() {
                Cars = _mapper.Map<IEnumerable<CarDto>, List<CarViewModel>>(source.Dtos),
                Filters = new CarFilterViewModel(),
                PageViewModel = new PageViewModel(source.PossibleDtosCount, filterModel.PageNumber, filterModel.TakeCount)
            };

            if (ModelState.IsValid) {
                model.SelectedBrand = filterModel.BrandId;
                model.SelectedModel = filterModel.ModelId;
                model.SelectedOrderType = filterModel.OrderType;
                model.Model = filterModel.Model;
            }

            return View(model);
        }

        // GET: CarsController/Details/5
        [HttpGet]
        [Route("Car/{id:int}")]
        public async Task<IActionResult> Details(int id) {

            var result = await _carService.GetCarByIdAsync(id);

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
        public async Task<IActionResult> Edit(int id) {

            var result = await _carService.GetCarByIdAsync(id);

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
        public async Task<IActionResult> Edit(CreateEditCarViewModel model) {

            var carDto = _mapper.Map<CreateEditCarViewModel, CarDto>(model);

            var directoryPath = Path.Combine(_appEnvironment.WebRootPath, "img", "cars");
            var result = await _carService.EditCarAsync(carDto, model.Images, directoryPath);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                GetInformationToCreateEditCar();

                var carGetResult = await _carService.GetCarByIdAsync(model.Id);

                var editModel = _mapper.Map<CarDto, CreateEditCarViewModel>(carGetResult.Result);
                var viewModel = _mapper.Map<CarDto, CarViewModel>(carGetResult.Result);

                GetInformationToCreateEditCar();
                ViewBag.EquipmentCategories = _mapper.Map<IEnumerable<EquipmentItemCategoryDto>, IEnumerable<EquipmentItemCategoryViewModel>>(_equipmentItemCategoryService.GetAllCategories());

                return View("Edit", new EditCarViewModel() { EditModel = editModel, ViewModel = viewModel });
            }

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        // GET: CarsController/Delete/5
        public async Task<IActionResult> Delete(int id) {
            var result = await _carService.GetCarByIdAsync(id);

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
    }
}
