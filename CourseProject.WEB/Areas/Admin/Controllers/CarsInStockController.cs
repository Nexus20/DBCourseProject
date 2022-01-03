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

namespace CourseProject.WEB.Areas.Admin.Controllers;

[Area("Admin")]
public class CarsInStockController : Controller {

    private readonly ICarInStockService _carInStockService;

    private readonly IMapper _mapper;

    public CarsInStockController(ICarInStockService carInStockService, IMapper mapper) {
        _carInStockService = carInStockService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index([FromQuery] CarInStockFilterViewModel filterModel) {

        var source = await _carInStockService.GetAllCarsInStockAsync(_mapper.Map<CarInStockFilterViewModel, CarInStockFilterModel>(filterModel));

        var model = new CarsInStockWithFiltersViewModel() {
            Cars = _mapper.Map<IEnumerable<CarInStockDto>, List<CarInStockViewModel>>(source.Dtos),
            Filters = new CarInStockFilterViewModel(),
            PageViewModel = new PageViewModel(source.PossibleDtosCount, filterModel.PageNumber, filterModel.TakeCount)
        };

        if (ModelState.IsValid) {
            model.SelectedBrand = filterModel.BrandId;
            model.SelectedModel = filterModel.ModelId;
            model.SelectedOrderType = filterModel.OrderType;
            model.Model = filterModel.Model;
            model.SelectedShowroom = filterModel.ShowroomId;
        }

        return View(model);
    }

    // GET: CarsController/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id) {

        var result = await _carInStockService.GetCarInStockByIdAsync(id);

        if (result.HasErrors) {
            TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
            return RedirectToAction(nameof(ErrorController.Error502), "Error");
        }

        var model = _mapper.Map<CarInStockDto, CarInStockViewModel>(result.Result);

        return View(model);
    }
}