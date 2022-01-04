using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.DTO.StatisticsDtos;
using CourseProject.BLL.Interfaces;
using CourseProject.Domain;
using CourseProject.WEB.Models;
using CourseProject.WEB.Models.StatisticsViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.WEB.Areas.Admin.Controllers {

    [Area("Admin")]
    public class StatisticsController : Controller {

        private readonly IStatisticsService _statisticsService;

        private readonly IMapper _mapper;

        public StatisticsController(IStatisticsService statisticsService, IMapper mapper) {
            _statisticsService = statisticsService;
            _mapper = mapper;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult GetTopManagersWhoHandleMoreOrders() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetTopManagersWhoHandleMoreOrders(TopManagersWhoHandleMoreOrdersSettingsViewModel settingsViewModel) {

            var source = await _statisticsService.GetTopManagersWhoCompletedMorePurchaseOrdersAsync(
                _mapper
                    .Map<TopManagersWhoHandleMoreOrdersSettingsViewModel, TopManagersWhoHandleMoreOrdersSettingsDto>(settingsViewModel));

            var model = _mapper.Map<IEnumerable<MaxPurchaseOrdersManagerDto>, List<MaxPurchaseOrdersManagerViewModel>>(source);

            return View("GetTopManagersWhoHandleMoreOrdersStatistics", model);
        }

        [HttpGet]
        public IActionResult GetTopClientsWhoMadeMoreOrders() {
            return View("MaxOrdersClientsTopSettings");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetTopClientsWhoMadeMoreOrders(int top) {

            var source = await _statisticsService.GetTopClientsWhoMadeMoreOrdersAsync(top);

            var model = _mapper.Map<IEnumerable<MaxOrdersClientDto>, List<MaxOrdersClientViewModel>>(source);

            return View(model);
        }

        [HttpGet]
        public IActionResult GetTopMostFrequentlyPurchasedCarModels() {
            return View("MaxOrdersCarModelsTopSettings");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetTopMostFrequentlyPurchasedCarModels(int top) {

            var source = await _statisticsService.GetTopMostPurchasedCarModelsAsync(top);

            var model = _mapper.Map<IEnumerable<MostPurchasedModelDto>, List<MostPurchasedModelViewModel>>(source);

            return View(model);
        }

        [HttpGet]
        public IActionResult GetProfit() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetProfit(DateRangeSettings settings) {

            var source = await _statisticsService.GetProfitAsync(settings);

            var model = _mapper.Map<OrdersProfitDto, OrdersProfitViewModel>(source);

            return View("ProfitStatistics", model);
        }
    }
}
