using System.Text.Json;
using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.WEB.Controllers;
using CourseProject.WEB.Extensions;
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
        public IActionResult GetTopClientsWhoMadeMoreOrders() {

            var dto = _statisticsService.GetTopClientsWhoMadeMoreOrders();

            var model = _mapper.Map<IEnumerable<ClientDto>, List<ClientViewModel>>(dto);

            return View(model);
        }

        [HttpGet]
        public IActionResult GetTopMostFrequentlyPurchasedCarModels() {

            var dto = _statisticsService.GetTopMostFrequentlyPurchasedCarModels();

            var model = _mapper.Map<IEnumerable<ModelDto>, List<ModelViewModel>>(dto);

            return View(model);
        }
    }
}
