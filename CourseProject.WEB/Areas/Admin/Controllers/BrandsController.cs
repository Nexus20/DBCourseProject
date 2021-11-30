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
    public class BrandsController : Controller {

        private readonly IBrandService _brandService;

        private readonly IMapper _mapper;

        public BrandsController(IBrandService brandService, IMapper mapper) {
            _brandService = brandService;
            _mapper = mapper;
        }


        // GET: BrandsController
        [HttpGet]
        [Route("/brands")]
        public IActionResult Index() {

            var source = _brandService.GetAllBrands();

            var model = _mapper.Map<IEnumerable<BrandDto>, List<BrandViewModel>>(source);

            return View(model);
        }

        // GET: BrandsController/Details/5
        [HttpGet]
        [Route("/brands/{id:int}")]
        public IActionResult Details(int id) {
            return View();
        }

        // GET: BrandsController/Create
        [HttpGet]
        [Route("/brands/new")]
        public IActionResult Create() {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [Route("/brands/new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditBrandViewModel model) {

            if (!ModelState.IsValid) {
                return View("Create", model);
            }

            var brandDto = _mapper.Map<CreateEditBrandViewModel, BrandDto>(model);

            var result = await _brandService.CreateBrandAsync(brandDto);

            if (result.HasErrors) {

                ModelState.AddErrorsFromOperationResult(result);

                return View("Create", model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: BrandsController/Edit/5
        public async Task<IActionResult> Edit(int id) {

            var result = await _brandService.GetBrandById(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<BrandDto, CreateEditBrandViewModel>(result.Result);

            return View(model);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEditBrandViewModel model) {

            if (!ModelState.IsValid) {
                return View("Edit", model);
            }

            var brandDto = _mapper.Map<CreateEditBrandViewModel, BrandDto>(model);

            var result = await _brandService.EditBrandAsync(brandDto);

            if (result.HasErrors) {

                ModelState.AddErrorsFromOperationResult(result);

                return View("Edit", model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: BrandsController/Delete/5
        public IActionResult Delete(int id) {
            return View();
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
