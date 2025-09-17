using FileUpload.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FileUpload.Controllers
{

    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("tv")]   // <<==== แบบนี้
        public IActionResult ViewImages()
        {
            return View();
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload(IFormFile img)
        {
            if (img != null && img.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", "1.jpg");
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }
            }
            return RedirectToAction("ViewImages", "Home");
        }
    }

}
