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
    public class CarsController : Controller {

        private readonly ICarService _carService;

        private readonly IModelService _modelService;

        private readonly IWebHostEnvironment _appEnvironment;

        private readonly IMapper _mapper;

        public CarsController(ICarService carService, IMapper mapper, IModelService modelService, IWebHostEnvironment appEnvironment) {
            _carService = carService;
            _mapper = mapper;
            _modelService = modelService;
            _appEnvironment = appEnvironment;
        }

        // GET: CarsController
        public IActionResult Index() {

            var source = _carService.GetAllCars();

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

            var brandDto = _mapper.Map<CreateEditCarViewModel, CarDto>(model);

            var directoryPath = Path.Combine(_appEnvironment.WebRootPath, "img", "cars");
            var result = await _carService.CreateCarAsync(brandDto, model.Images, directoryPath);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                GetInformationToCreateEditCar();
                return View("Create", model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CarsController/Edit/5
        public IActionResult Edit(int id) {
            return View();
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
    }
}
