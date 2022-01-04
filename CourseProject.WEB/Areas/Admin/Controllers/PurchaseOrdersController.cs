﻿using System.Text.Json;
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

namespace CourseProject.WEB.Areas.Admin.Controllers; 

[Area("Admin")]
public class PurchaseOrdersController : Controller {

    private readonly IPurchaseOrderService _purchaseOrderService;

    private readonly IMapper _mapper;

    private readonly IViewRenderService _viewRenderService;

    private readonly IConverter _converter;

    private readonly IManagerService _managerService;

    public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService, IMapper mapper, IViewRenderService viewRenderService, IConverter converter, IManagerService managerService) {
        _purchaseOrderService = purchaseOrderService;
        _mapper = mapper;
        _viewRenderService = viewRenderService;
        _converter = converter;
        _managerService = managerService;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] PurchaseOrderFilterViewModel filterModel) {

        var managerResult = await _managerService.GetManagerByClaimsAsync(User);

        if (managerResult.HasErrors) {
            TempData["Errors"] = JsonSerializer.Serialize(managerResult.Errors);
            return RedirectToAction(nameof(ErrorController.Error502), "Error");
        }

        filterModel.ShowroomId ??= managerResult.Result.ShowroomId;

        var source = await _purchaseOrderService.GetAllPurchaseOrdersAsync(_mapper.Map<PurchaseOrderFilterViewModel, PurchaseOrderFilterModel>(filterModel));

        var model = new PurchaseOrdersWithFiltersViewModel() {
            PurchaseOrders = _mapper.Map<IEnumerable<PurchaseOrderDto>, List<PurchaseOrderViewModel>>(source.Dtos),
            Filters = new PurchaseOrderFilterViewModel(),
            PageViewModel = new PageViewModel(source.PossibleDtosCount, filterModel.PageNumber, filterModel.TakeCount)
        };

        if (ModelState.IsValid) {
            model.SelectedOrderId = filterModel.OrderId;
            model.SelectedOrderType = filterModel.OrderType;
            model.SelectedClientEmail = filterModel.ClientEmail;
            model.SelectedClientPhone = filterModel.ClientPhone;

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

        var result = await _purchaseOrderService.GetOrderByIdAsync(id);

        if (result.HasErrors) {
            TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
            return RedirectToAction(nameof(ErrorController.Error502), "Error");
        }

        var model = _mapper.Map<PurchaseOrderDto, PurchaseOrderViewModel>(result.Result);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AcceptPurchaseOrder(int purchaseOrderId) {

        var result = await _purchaseOrderService.AcceptOrderAsync(purchaseOrderId, User);

        if (result.HasErrors) {
            TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
            return RedirectToAction(nameof(ErrorController.Error502), "Error");
        }

        return RedirectToAction(nameof(Details), new { id = purchaseOrderId });
    }

    [HttpGet]
    public async Task<IActionResult> ClosePurchaseOrder(int purchaseOrderId) {

        var result = await _purchaseOrderService.GetOrderByIdAsync(purchaseOrderId);

        if (result.HasErrors) {
            TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
            return RedirectToAction(nameof(ErrorController.Error502), "Error");
        }

        var model = _mapper.Map<PurchaseOrderDto, PurchaseOrderViewModel>(result.Result);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmClosePurchaseOrder(int purchaseOrderId) {

        var result = await _purchaseOrderService.CloseOrderAsync(purchaseOrderId);

        if (result.HasErrors) {
            TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
            return RedirectToAction(nameof(ErrorController.Error502), "Error");
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> CancelPurchaseOrder(int purchaseOrderId) {

        var result = await _purchaseOrderService.GetOrderByIdAsync(purchaseOrderId);

        if (result.HasErrors) {
            TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
            return RedirectToAction(nameof(ErrorController.Error502), "Error");
        }

        var model = _mapper.Map<PurchaseOrderDto, PurchaseOrderViewModel>(result.Result);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmCancelPurchaseOrder(int purchaseOrderId) {

        var result = await _purchaseOrderService.CancelOrderAsync(purchaseOrderId);

        if (result.HasErrors) {
            TempData["Errors"] = JsonSerializer.Serialize(result.Errors);
            return RedirectToAction(nameof(ErrorController.Error502), "Error");
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> CreateSalesReport() {

        var dto = _purchaseOrderService.GetAllOrders();

        var model = new SalesReportViewModel() {
            PurchaseOrders = _mapper.Map<IEnumerable<PurchaseOrderDto>, List<PurchaseOrderViewModel>>(dto)
        };

        return File(await CreatePdf(model), "application/pdf");
    }

    private async Task<byte[]> CreatePdf(SalesReportViewModel model) {

        var globalSettings = new GlobalSettings {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings { Top = 10 },
            DocumentTitle = "Purchase orders report"
        };

        var objectSettings = new ObjectSettings {
            PagesCount = true,
            HtmlContent = await _viewRenderService.RenderToString("PurchaseOrders/SalesReport", model),
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