using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Models;
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
        public async Task<IActionResult> GetTopClientsWhoMadeMoreOrders() {

            var source = await _statisticsService.GetTopClientsWhoMadeMoreOrdersAsync();

            var model = _mapper.Map<IEnumerable<MaxOrdersClientDto>, List<MaxOrdersClientViewModel>>(source);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetTopMostFrequentlyPurchasedCarModels() {

            var source = await _statisticsService.GetTopMostPurchasedCarModelsAsync();

            var model = _mapper.Map<IEnumerable<MostPurchasedModelDto>, List<MostPurchasedModelViewModel>>(source);

            return View(model);
        }
    }
}
