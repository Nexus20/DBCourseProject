using AutoMapper;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.ReportDtos;
using CourseProject.Domain;
using CourseProject.WEB.Models;
using CourseProject.WEB.Models.ReportViewModels;
using CourseProject.WEB.Utils;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.WEB.Areas.Admin.Controllers; 

[Area("Admin")]
public class ReportsController : Controller {

    private readonly IReportService _reportService;

    private readonly IMapper _mapper;

    private readonly IViewRenderService _viewRenderService;

    private readonly IConverter _converter;

    public ReportsController(IReportService reportService, IMapper mapper, IViewRenderService viewRenderService, IConverter converter) {
        _reportService = reportService;
        _mapper = mapper;
        _viewRenderService = viewRenderService;
        _converter = converter;
    }

    public IActionResult Index() {
        return View();
    }

    [HttpGet]
    public IActionResult GetPurchaseOrdersReport() {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GetPurchaseOrdersReport(DateRangeSettings dateSettings, int showroomId) {

        var source = await _reportService.GetPurchaseOrdersReport(dateSettings, showroomId);

        var model = _mapper.Map<PurchaseOrdersReportDto, PurchaseOrdersReportViewModel>(source);

        return File(await CreatePdf(model, "PurchaseOrdersReportPdf", "Purchase orders report"), "application/pdf");
    }

    [HttpGet]
    public IActionResult GetSupplyOrdersReport() {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GetSupplyOrdersReport(DateRangeSettings dateSettings, int showroomId, int supplierId) {

        var source = await _reportService.GetSupplyOrdersReport(dateSettings, showroomId, supplierId);

        var model = _mapper.Map<SupplyOrdersReportDto, SupplyOrdersReportViewModel>(source);

        return File(await CreatePdf(model, "SupplyOrdersReportPdf", "Supply orders report"), "application/pdf");
    }

    private async Task<byte[]> CreatePdf<TModel>(TModel model, string viewName, string documentTitle) where TModel : class{

        var globalSettings = new GlobalSettings {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Landscape,
            PaperSize = PaperKind.A3,
            Margins = new MarginSettings { Top = 10 },
            DocumentTitle = documentTitle
        };

        var objectSettings = new ObjectSettings {
            PagesCount = true,
            HtmlContent = await _viewRenderService.RenderToString(viewName, model),
            WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lib", "twitter-bootstrap", "css", "bootstrap.css") },
            HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
            FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report" }
        };

        var pdf = new HtmlToPdfDocument() {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        };

        return _converter.Convert(pdf);
    }
}