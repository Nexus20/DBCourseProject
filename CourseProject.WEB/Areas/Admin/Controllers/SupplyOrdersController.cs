using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Controllers;
using CourseProject.WEB.Models;
using CourseProject.WEB.Models.FilterViewModels;
using CourseProject.WEB.Models.PaginatedFilteredViewModels;
using CourseProject.WEB.Models.SupplyOrders;
using CourseProject.WEB.Utils;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.Areas.Admin.Controllers {

    [Area("Admin")]
    public class SupplyOrdersController : Controller {

        private readonly ICarService _carService;

        private readonly ISupplyOrderService _supplyOrderService;

        private readonly IMapper _mapper;

        private readonly IViewRenderService _viewRenderService;

        private readonly IConverter _converter;

        private readonly IManagerService _managerService;

        private readonly ISupplierService _supplierService;

        public SupplyOrdersController(ISupplyOrderService supplyOrderService, IMapper mapper, IViewRenderService viewRenderService, IConverter converter, ICarService carService, IManagerService managerService, ISupplierService supplierService) {
            _supplyOrderService = supplyOrderService;
            _mapper = mapper;
            _viewRenderService = viewRenderService;
            _converter = converter;
            _carService = carService;
            _managerService = managerService;
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(SupplyOrderViewModel viewModel) {

            var result = await _managerService.GetManagerByClaimsAsync(User);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            GetInformationToCreateEditSupplyOrder();

            var model = new SupplyOrderViewModel {
                ManagerId = result.Result.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirm(SupplyOrderViewModel viewModel) {

            if (!ModelState.IsValid) {
                GetInformationToCreateEditSupplyOrder(viewModel.SupplierId);
                return View("Create", viewModel);
            }

            var result = await _supplyOrderService.CreateOrderAsync(_mapper.Map<SupplyOrderViewModel, SupplyOrderDto>(viewModel));

            if (result.HasErrors) {
                GetInformationToCreateEditSupplyOrder(viewModel.SupplierId);
                return View("Create", viewModel);
            }

            return RedirectToAction(nameof(AddCarsToSupplyOrder), new { supplyOrderId = result.Result });
        }

        [HttpGet]
        public async Task<IActionResult> AddCarsToSupplyOrder(int supplyOrderId) {

            var result = await _supplyOrderService.GetOrderByIdAsync(supplyOrderId);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var filterModel = new CarFilterModel() {
                BrandId = (uint)result.Result.Supplier.BrandId
            };

            var carsToShow = await _carService.GetAllCarsAsync(filterModel);

            var model = _mapper.Map<IEnumerable<CarDto>, List<CarViewModel>>(carsToShow.Dtos);
            ViewBag.SupplyOrderId = supplyOrderId;

            return View("PossibleCarsToAddToOrder", model);
        }

        [HttpGet]
        public async Task<IActionResult> AddCarToSupplyOrder(int carId, int supplyOrderId) {

            var result = await _carService.GetCarByIdAsync(carId);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var supplyOrderResult = await _supplyOrderService.GetOrderByIdAsync(supplyOrderId);

            if (supplyOrderResult.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(supplyOrderResult.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = new CreateSupplyOrderPartViewModel() {
                Items = new List<SupplyOrderPartItem>(),
                Car = _mapper.Map<CarDto, CarViewModel>(result.Result),
                SupplyOrderId = supplyOrderId
            };

            foreach (var equipmentItem in result.Result.EquipmentItems) {

                model.Items.Add(new SupplyOrderPartItem() {
                    SelectList = new SelectList(
                        _mapper.Map<IEnumerable<EquipmentItemValueDto>, IEnumerable<EquipmentItemValueViewModel>>(
                            equipmentItem.EquipmentItemValues), "Id", "Value"),
                    EquipmentItemCategoryName = equipmentItem.Category.Name
                });
            }

            return View("ConfigureCarToAddToSupplyOrder", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCarToSupplyOrder(CreateSupplyOrderPartViewModel viewModel) {

            if (!ModelState.IsValid) {
                return View("ConfigureCarToAddToSupplyOrder", viewModel);
            }

            var managerFindResult = await _managerService.GetManagerByClaimsAsync(User);

            if (managerFindResult.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(managerFindResult.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var result = await _supplyOrderService.AddCarToSupplyOrderAsync(viewModel.SupplyOrderId, viewModel.Equipment ?? Array.Empty<int>(), viewModel.Count, managerFindResult.Result.Id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Details), new {id = viewModel.SupplyOrderId});
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] SupplyOrderFilterViewModel filterModel) {

            var result = await _managerService.GetManagerByClaimsAsync(User);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            filterModel.ManagerId = result.Result.Id.ToString();

            var source = await _supplyOrderService.GetAllSupplyOrdersAsync(_mapper.Map<SupplyOrderFilterViewModel, SupplyOrderFilterModel>(filterModel));

            var model = new SupplyOrdersWithFiltersViewModel() {
                SupplyOrders = _mapper.Map<IEnumerable<SupplyOrderDto>, List<SupplyOrderViewModel>>(source.Dtos),
                Filters = new SupplyOrderFilterViewModel(),
                PageViewModel = new PageViewModel(source.PossibleDtosCount, filterModel.PageNumber, filterModel.TakeCount)
            };

            if (ModelState.IsValid) {
                model.SelectedOrderId = filterModel.OrderId;
                model.SelectedSupplierId = filterModel.SupplierId;
                model.SelectedOrderType = filterModel.OrderType;
                model.SelectedSupplierEmail = filterModel.SupplierEmail;
                model.SelectedSupplierName = filterModel.SupplierName;
                model.SelectedSupplierPhone = filterModel.SupplierPhone;

                if (filterModel.CreationDate.HasValue) {
                    var selectedDay = filterModel.CreationDate.Value.Day.ToString();
                    if (selectedDay.Length == 1) {
                        selectedDay = "0" + selectedDay;
                    }
                    var selectedMonth = filterModel.CreationDate.Value.Month.ToString();
                    if (selectedMonth.Length == 1) {
                        selectedMonth = "0" + selectedMonth;
                    }
                    model.SelectedCreationDate = $"{filterModel.CreationDate.Value.Year}-{selectedMonth}-{selectedDay}";
                }

                if (filterModel.LastUpdateDate.HasValue) {
                    var selectedDay = filterModel.LastUpdateDate.Value.Day.ToString();
                    if (selectedDay.Length == 1) {
                        selectedDay = "0" + selectedDay;
                    }
                    var selectedMonth = filterModel.LastUpdateDate.Value.Month.ToString();
                    if (selectedMonth.Length == 1) {
                        selectedMonth = "0" + selectedMonth;
                    }
                    model.SelectedLastUpdateDate = $"{filterModel.LastUpdateDate.Value.Year}-{selectedMonth}-{selectedDay}";
                }

                model.SelectedState = filterModel.State;
                model.SelectedManagerId = filterModel.ManagerId;
            }

            return View(model);
        }

        // GET: BrandsController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id) {

            var result = await _supplyOrderService.GetOrderByIdAsync(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<SupplyOrderDto, SupplyOrderViewModel>(result.Result);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SendSupplyOrder(int supplyOrderId) {

            var result = await _supplyOrderService.GetOrderByIdAsync(supplyOrderId);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var sendingOrderResult = await _supplyOrderService.SendSupplyOrderAsync(supplyOrderId);

            if (sendingOrderResult.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(sendingOrderResult.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Details), new { id = supplyOrderId });
        }

        [HttpGet]
        public async Task<IActionResult> CloseSupplyOrder(int supplyOrderId) {

            var result = await _supplyOrderService.GetOrderByIdAsync(supplyOrderId);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<SupplyOrderDto, SupplyOrderViewModel>(result.Result);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CloseSupplyOrder(CloseSupplyOrderViewModel viewModel) {

            var dto = new CloseSupplyOrderDto() {
                ManagerId = viewModel.ManagerId,
                SupplyOrderId = viewModel.SupplyOrderId,
                Parts = JsonSerializer.Deserialize<ClosingSupplyOrderDtoPart[]>(viewModel.JsonSupplyOrderParts)
            };

            var result = await _supplyOrderService.TakeCarsToShowroom(dto);

            if (result.HasErrors) {
                return BadRequest(JsonSerializer.Serialize(result.Errors));
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmCloseSupplyOrder(int supplyOrderId) {
            var result = await _supplyOrderService.GetOrderByIdAsync(supplyOrderId);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var closingOrderResult = await _supplyOrderService.CloseSupplyOrderAsync(supplyOrderId);

            if (closingOrderResult.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(closingOrderResult.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            return RedirectToAction(nameof(Index));
        }

        //public IActionResult CreateSalesReport() {

        //    var dto = _purchaseOrderService.GetAllOrders();

        //    var model = new SalesReportViewModel() {
        //        PurchaseOrders = _mapper.Map<IEnumerable<PurchaseOrderDto>, List<PurchaseOrderViewModel>>(dto)
        //    };

        //    return File(CreatePdf(model), "application/pdf");
        //}

        private byte[] CreatePdf(SalesReportViewModel model) {

            var globalSettings = new GlobalSettings {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Invoice"
            };

            var objectSettings = new ObjectSettings {
                PagesCount = true,
                HtmlContent = _viewRenderService.RenderToString("PurchaseOrders/SalesReport", model),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "pdf.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Invoice" }
            };

            var pdf = new HtmlToPdfDocument() {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return _converter.Convert(pdf);
        }

        private void GetInformationToCreateEditSupplyOrder(int? selectedValue = null) {

            var source = _supplierService.GetAllSuppliers();

            ViewBag.Suppliers = new SelectList(_mapper.Map<IEnumerable<SupplierDto>, IEnumerable<SupplierViewModel>>(source), "Id", "Name", selectedValue);
        }
    }
}
