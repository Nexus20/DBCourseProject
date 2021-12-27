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
    public class ModelsController : Controller {

        private readonly IBrandService _brandService;

        private readonly IModelService _modelService;

        private readonly IMapper _mapper;

        public ModelsController(IModelService modelService, IMapper mapper, IBrandService brandService) {
            _modelService = modelService;
            _mapper = mapper;
            _brandService = brandService;
        }


        // GET: ModelsController
        [HttpGet]
        [Route("/models")]
        public async Task<IActionResult> Index([FromQuery] ModelFilterViewModel filterModel) {

            var source = await _modelService.GetAllModelsAsync(_mapper.Map<ModelFilterViewModel, ModelFilterModel>(filterModel));

            var model = new ModelsWithFiltersViewModel() {
                Models = _mapper.Map<IEnumerable<ModelDto>, List<ModelViewModel>>(source.Dtos),
                Filters = new ModelFilterViewModel(),
                PageViewModel = new PageViewModel(source.PossibleDtosCount, filterModel.PageNumber, filterModel.TakeCount)
            };

            if (ModelState.IsValid) {
                model.SelectedBrand = filterModel.BrandId;
                model.SelectedOrderType = filterModel.OrderType;
                model.Model = filterModel.Model;
            }

            return View(model);
        }

        // GET: ModelsController/Details/5
        [HttpGet]
        [Route("/models/{id:int}")]
        public IActionResult Details(int id) {
            return View();
        }

        // GET: ModelsController/Create
        [HttpGet]
        [Route("/models/new")]
        public IActionResult Create() {
            GetInformationToCreateEditModel();
            return View();
        }

        // POST: ModelsController/Create
        [HttpPost]
        [Route("/models/new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditModelViewModel model) {

            if (!ModelState.IsValid) {
                GetInformationToCreateEditModel();
                return View("Create", model);
            }

            var modelDto = _mapper.Map<CreateEditModelViewModel, ModelDto>(model);

            var result = await _modelService.CreateModelAsync(modelDto);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                GetInformationToCreateEditModel();
                return View("Create", model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ModelsController/Edit/5
        public async Task<IActionResult> Edit(int id) {

            var result = await _modelService.GetModelById(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            GetInformationToCreateEditModel();
            var model = _mapper.Map<ModelDto, CreateEditModelViewModel>(result.Result);

            return View(model);
        }

        // POST: ModelsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEditModelViewModel model) {

            if (!ModelState.IsValid) {
                GetInformationToCreateEditModel();
                return View("Edit", model);
            }

            var modelDto = _mapper.Map<CreateEditModelViewModel, ModelDto>(model);

            var result = await _modelService.EditModelAsync(modelDto);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                GetInformationToCreateEditModel();
                return View("Edit", model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ModelsController/Delete/5
        public async Task<IActionResult> Delete(int id) {

            var result = await _modelService.GetModelById(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<ModelDto, ModelViewModel>(result.Result);

            return View(model);
        }

        // POST: ModelsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id) {

            var result = await _modelService.DeleteModelAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Index));
        }

        private void GetInformationToCreateEditModel() {

            var brands = _brandService.GetAllBrands();

            ViewBag.Brands = new SelectList(_mapper.Map<IEnumerable<BrandDto>, IEnumerable<BrandViewModel>>(brands), "Id", "Name");
        }

    }
}
