using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Controllers;
using CourseProject.WEB.Models;
using CourseProject.WEB.Models.FilterViewModels;
using CourseProject.WEB.Models.PaginatedFilteredViewModels;
using CourseProject.WEB.Utils;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.WEB.Areas.Admin.Controllers {

    [Area("Admin")]
    public class SupplyOrdersController : Controller {

        private readonly ISupplyOrderService _supplyOrderService;

        private readonly IMapper _mapper;

        private readonly IViewRenderService _viewRenderService;

        private readonly IConverter _converter;

        public SupplyOrdersController(ISupplyOrderService supplyOrderService, IMapper mapper, IViewRenderService viewRenderService, IConverter converter) {
            _supplyOrderService = supplyOrderService;
            _mapper = mapper;
            _viewRenderService = viewRenderService;
            _converter = converter;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] SupplyOrderFilterViewModel filterModel) {

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

            var result = await _supplyOrderService.GetOrderById(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<SupplyOrderDto, SupplyOrderViewModel>(result.Result);

            return View(model);
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
    }
}
