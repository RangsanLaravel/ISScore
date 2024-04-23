using ISScore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ISScore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Search search)
        {
            if (!ModelState.IsValid)
            {
                return View(search);
            }
            RenderDetail detail = new RenderDetail { Name = "Mr.Sivawut Tatan",
                Employee_id =search.EMPLOYEE_ID,
                Desk_Check1 ="0",
                Desk_Check2="0",
                HQ_Mock_test ="1"
            };
            ViewBag.DataSearch = detail;
            return View(search);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
