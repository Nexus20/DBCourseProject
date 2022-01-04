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
using CourseProject.WEB.Utils;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.Controllers {
    public class HomeController : Controller {

        private readonly ICarService _carService;

        private readonly IMapper _mapper;

        private readonly IPurchaseOrderService _purchaseOrderService;

        private readonly IUserService _userService;

        private readonly IShowroomService _showroomService;

        private readonly IViewRenderService _viewRenderService;

        private readonly IConverter _converter;

        public HomeController(ICarService carService, IMapper mapper, IPurchaseOrderService purchaseOrderService, IUserService userService, IViewRenderService viewRenderService, IConverter converter, IShowroomService showroomService) {
            _carService = carService;
            _mapper = mapper;
            _purchaseOrderService = purchaseOrderService;
            _userService = userService;
            _viewRenderService = viewRenderService;
            _converter = converter;
            _showroomService = showroomService;
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
                ClientPersonalDataViewModel = new ClientPersonalDataViewModel(),
                ClientId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            if (!string.IsNullOrWhiteSpace(model.ClientId)) {

                var userResult = await _userService.GetClientByIdAsync(model.ClientId);

                model.ClientPersonalDataViewModel.Email = userResult.Result.Email;
                model.ClientPersonalDataViewModel.Name = userResult.Result.Name;
                model.ClientPersonalDataViewModel.Surname = userResult.Result.Surname;
                model.ClientPersonalDataViewModel.Patronymic = userResult.Result.Patronymic;
                model.ClientPersonalDataViewModel.Phone = userResult.Result.PhoneNumber;
            }

            foreach (var equipmentItem in result.Result.EquipmentItems) {

                model.Items.Add(new PurchaseOrderItem() {
                    SelectList = new SelectList(
                        _mapper.Map<IEnumerable<EquipmentItemValueDto>, IEnumerable<EquipmentItemValueViewModel>>(
                            equipmentItem.EquipmentItemValues), "Id", "ValueWithPrice"),
                    EquipmentItemCategoryName = equipmentItem.Category.Name
                });
            }

            ViewBag.Showrooms =
                new SelectList(
                    _mapper.Map<IEnumerable<ShowroomDto>, IEnumerable<ShowroomViewModel>>(_showroomService
                        .GetAllShowrooms()), "Id", "Address");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrder(string clientId, int[] equipment, int showroomId, ClientPersonalDataViewModel clientPersonalDataViewModel) {

            var result = await _purchaseOrderService.CreateOrderAsync(clientId, equipment, showroomId, _mapper.Map<ClientPersonalDataViewModel, ClientPersonalDataDto>(clientPersonalDataViewModel));

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(PurchaseOrderCreatedSuccessful), new {id = result.Result});
        }

        [HttpGet]
        public async Task<IActionResult> PurchaseOrderCreatedSuccessful(int id) {

            var result = await _purchaseOrderService.GetOrderByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<PurchaseOrderDto, PurchaseOrderViewModel>(result.Result);

            return View(model);
        }

        public async Task<IActionResult> CreatePurchaseOrderPdf(int id) {

            var result = await _purchaseOrderService.GetOrderByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<PurchaseOrderDto, PurchaseOrderViewModel>(result.Result);

            return File(await CreatePdf(model), "application/pdf");
        }

        private async Task<byte[]> CreatePdf(PurchaseOrderViewModel model) {

            var globalSettings = new GlobalSettings {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Purchase order"
            };

            var objectSettings = new ObjectSettings {
                PagesCount = true,
                HtmlContent = await _viewRenderService.RenderToString("PurchaseOrderPdf", model),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lib", "twitter-bootstrap", "css", "bootstrap.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Invoice" }
            };

            var pdf = new HtmlToPdfDocument() {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return _converter.Convert(pdf);
        }
    }
}