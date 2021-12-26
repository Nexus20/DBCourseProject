using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Controllers;
using CourseProject.WEB.Models;
using CourseProject.WEB.Utils;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.WEB.Areas.Admin.Controllers {
    [Area("Admin")]
    public class PurchaseOrdersController : Controller {

        private readonly IPurchaseOrderService _purchaseOrderService;

        private readonly IMapper _mapper;

        private readonly IViewRenderService _viewRenderService;

        private readonly IConverter _converter;

        public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService, IMapper mapper, IViewRenderService viewRenderService, IConverter converter) {
            _purchaseOrderService = purchaseOrderService;
            _mapper = mapper;
            _viewRenderService = viewRenderService;
            _converter = converter;
        }

        [HttpGet]
        public IActionResult Index() {

            var source = _purchaseOrderService.GetAllOrders();

            var model = _mapper.Map<IEnumerable<PurchaseOrderDto>, List<PurchaseOrderViewModel>>(source);

            return View(model);
        }

        // GET: BrandsController/Details/5
        [HttpGet]
        public IActionResult Details(int id) {

            var result = _purchaseOrderService.GetOrderById(id);

            if (result.HasErrors) {
                TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
                return RedirectToAction(nameof(ErrorController.Error502), "Error");
            }

            var model = _mapper.Map<PurchaseOrderDto, PurchaseOrderViewModel>(result.Result);

            return View(model);
        }

        public IActionResult CreateSalesReport() {

            var dto = _purchaseOrderService.GetAllOrders();

            var model = new SalesReportViewModel() {
                PurchaseOrders = _mapper.Map<IEnumerable<PurchaseOrderDto>, List<PurchaseOrderViewModel>>(dto)
            };

            return File(CreatePdf(model), "application/pdf");
        }

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
