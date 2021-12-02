using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Controllers;
using CourseProject.WEB.Extensions;
using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.WEB.Areas.Admin.Controllers {
    [Area("Admin")]
    public class EquipmentItemCategoriesController : Controller {

        private readonly IEquipmentItemCategoryService _equipmentItemCategoryService;

        private readonly IMapper _mapper;

        public EquipmentItemCategoriesController(IEquipmentItemCategoryService equipmentItemCategoryService, IMapper mapper) {
            _equipmentItemCategoryService = equipmentItemCategoryService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Index() {

            var source = _equipmentItemCategoryService.GetAllCategories();

            var model = _mapper.Map<IEnumerable<EquipmentItemCategoryDto>, List<EquipmentItemCategoryViewModel>>(source);

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id) {
            return View();
        }

        [HttpGet]
        public IActionResult Create() {
            var model = new CreateEditEquipmentItemCategoryViewModel {
                UnitsOfMeasure = "none",
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditEquipmentItemCategoryViewModel model) {

            if (!ModelState.IsValid) {
                return View("Create", model);
            }

            var categoryDto = _mapper.Map<CreateEditEquipmentItemCategoryViewModel, EquipmentItemCategoryDto>(model);

            var result = await _equipmentItemCategoryService.CreateCategoryAsync(categoryDto);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                return View("Create", model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id) {

            var result = await _equipmentItemCategoryService.GetCategoryById(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<EquipmentItemCategoryDto, CreateEditEquipmentItemCategoryViewModel>(result.Result);

            return View(model);
        }

        // POST: ModelsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEditEquipmentItemCategoryViewModel model) {

            if (!ModelState.IsValid) {
                return View("Edit", model);
            }

            var categoryDto = _mapper.Map<CreateEditEquipmentItemCategoryViewModel, EquipmentItemCategoryDto>(model);

            var result = await _equipmentItemCategoryService.EditCategoryAsync(categoryDto);

            if (result.HasErrors) {
                ModelState.AddErrorsFromOperationResult(result);
                return View("Edit", model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ModelsController/Delete/5
        public async Task<IActionResult> Delete(int id) {

            var result = await _equipmentItemCategoryService.GetCategoryById(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<EquipmentItemCategoryDto, EquipmentItemCategoryViewModel>(result.Result);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id) {

            var result = await _equipmentItemCategoryService.DeleteCategoryAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
