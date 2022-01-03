using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Models.FilterViewModels;
using CourseProject.WEB.Models.PaginatedFilteredViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.Controllers {
    public class HomeController : Controller {

        private readonly ICarService _carService;

        private readonly IMapper _mapper;

        private readonly IPurchaseOrderService _purchaseOrderService;

        public HomeController(ICarService carService, IMapper mapper, IPurchaseOrderService purchaseOrderService) {
            _carService = carService;
            _mapper = mapper;
            _purchaseOrderService = purchaseOrderService;
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> Details(int id) {

            var result = await _carService.GetCarByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<CarDto, CarViewModel>(result.Result);

            return View(model);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> CreatePurchaseOrder(int carId) {

            var result = await _carService.GetCarByIdAsync(carId);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = new CreatePurchaseOrderViewModel() {
                Items = new List<PurchaseOrderItem>(),
                ClientId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            foreach (var equipmentItem in result.Result.EquipmentItems) {

                model.Items.Add(new PurchaseOrderItem() {
                    SelectList = new SelectList(
                        _mapper.Map<IEnumerable<EquipmentItemValueDto>, IEnumerable<EquipmentItemValueViewModel>>(
                            equipmentItem.EquipmentItemValues), "Id", "Value"),
                    EquipmentItemCategoryName = equipmentItem.Category.Name
                });
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrder(string clientId, int[] equipment) {

            var result = await _purchaseOrderService.CreateOrderAsync(clientId, equipment);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(AccountController.Cabinet), "Account");
        }
    }
}