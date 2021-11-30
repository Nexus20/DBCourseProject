using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.WEB.Controllers {
    public class ErrorController : Controller {

        [HttpGet]
        [Route("/error/{statusCode}")]
        public IActionResult Index(HttpStatusCode statusCode) {

            return statusCode switch {
                HttpStatusCode.NotFound => RedirectToAction(nameof(Error404)),
                HttpStatusCode.BadRequest => RedirectToAction(nameof(Error502)),
                HttpStatusCode.InternalServerError => RedirectToAction(nameof(Error500)),
                _ => Content("Something went wrong")
            };
        }

        [HttpGet]
        [Route("/error/404")]
        public IActionResult Error404() {
            return View();
        }

        [HttpGet]
        [Route("/error/502")]
        public IActionResult Error502() {

            var errors = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(TempData.Peek("Errors") as string);

            return View(errors);
        }

        [HttpGet]
        [Route("/error/500")]
        public IActionResult Error500() {

            return View();
        }
    }

}
