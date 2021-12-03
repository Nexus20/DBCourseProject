using CourseProject.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.Controllers {
    public class HomeController : Controller {

        private readonly ICarService _carService;

        private readonly IModelService _modelService;

        private readonly IBrandService _brandService;

        private readonly IWebHostEnvironment _appEnvironment;

        private readonly IMapper _mapper;

        private readonly IPurchaseOrderService _purchaseOrderService;

        public HomeController(ICarService carService, IModelService modelService, IBrandService brandService, IWebHostEnvironment appEnvironment, IMapper mapper, IPurchaseOrderService purchaseOrderService) {
            _carService = carService;
            _modelService = modelService;
            _brandService = brandService;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
            _purchaseOrderService = purchaseOrderService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] CarFilterModel filterModel) {

            GetInfoToCreateFilters();

            var source = filterModel.IsReset ? _carService.GetAllCars() : _carService.GetAllCars(filterModel);

            var model = _mapper.Map<IEnumerable<CarDto>, List<CarViewModel>>(source);

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id) {

            var result = _carService.GetCarById(id);

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

        private void GetInfoToCreateFilters() {
            ViewBag.Brands = new SelectList(_mapper.Map<IEnumerable<BrandDto>, IEnumerable<BrandViewModel>>(_brandService.GetAllBrands()), "Id", "Name");
            ViewBag.Models = new SelectList(_mapper.Map<IEnumerable<ModelDto>, IEnumerable<ModelViewModel>>(_modelService.GetAllModels()), "Id", "NameWithBrand");
        }

        public IActionResult CreatePurchaseOrder(int carId) {

            var result = _carService.GetCarById(carId);

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