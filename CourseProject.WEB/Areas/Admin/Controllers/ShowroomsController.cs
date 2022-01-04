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

namespace CourseProject.WEB.Areas.Admin.Controllers {

    [Area("Admin")]
    public class ShowroomsController : Controller {

        private readonly IShowroomService _showroomService;

        private readonly IMapper _mapper;

        public ShowroomsController(IShowroomService showroomService, IMapper mapper) {
            _showroomService = showroomService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] ShowroomFilterViewModel filterModel) {

            var source = await _showroomService.GetAllShowroomsAsync(_mapper.Map<ShowroomFilterViewModel, ShowroomFilterModel>(filterModel));

            var model = new ShowroomsWithFiltersViewModel() {
                Showrooms = _mapper.Map<IEnumerable<ShowroomDto>, List<ShowroomViewModel>>(source.Dtos),
                Filters = new ShowroomFilterViewModel(),
                PageViewModel = new PageViewModel(source.PossibleDtosCount, filterModel.PageNumber, filterModel.TakeCount)
            };

            if (ModelState.IsValid) {
                model.SelectedAddress = filterModel.Address;
                model.SelectedPhone = filterModel.Phone;
                model.SelectedOrderType = filterModel.OrderType;
            }

            return View(model);
        }

        // GET: BrandsController/Details/5
        [HttpGet]
        [Route("/showrooms/{id:int}")]
        public async Task<IActionResult> Details(int id) {

            var result = await _showroomService.GetShowroomByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<ShowroomDto, ShowroomViewModel>(result.Result);

            return View(model);
        }

        // GET: BrandsController/Create
        [HttpGet]
        [Route("/showrooms/new")]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [Route("/showrooms/new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditShowroomViewModel model) {

            if (!ModelState.IsValid) {
                return View("Create", model);
            }

            var dto = _mapper.Map<CreateEditShowroomViewModel, ShowroomDto>(model);

            var result = await _showroomService.CreateShowroomAsync(dto);

            if (result.HasErrors) {

                ModelState.AddErrorsFromOperationResult(result);

                return View("Create", model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: BrandsController/Edit/5
        public async Task<IActionResult> Edit(int id) {

            var result = await _showroomService.GetShowroomByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<ShowroomDto, CreateEditShowroomViewModel>(result.Result);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEditShowroomViewModel model) {

            if (!ModelState.IsValid) {
                return View("Edit", model);
            }

            var dto = _mapper.Map<CreateEditShowroomViewModel, ShowroomDto>(model);

            var result = await _showroomService.EditShowroomAsync(dto);

            if (result.HasErrors) {

                ModelState.AddErrorsFromOperationResult(result);

                return View("Edit", model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: BrandsController/Delete/5
        public async Task<IActionResult> Delete(int id) {

            var result = await _showroomService.GetShowroomByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<ShowroomDto, ShowroomViewModel>(result.Result);

            return View(model);
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id) {

            var result = await _showroomService.DeleteShowroomAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
