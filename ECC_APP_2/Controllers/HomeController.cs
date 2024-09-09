using ECC_APP_2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECC_APP_2.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Student_registration()
        {
            return View();
        }

        public IActionResult Student_LogIn()
        {
            return View();
        }

        public IActionResult UploadCentre()
        {
            return View();
        }
        public IActionResult Networking()
        {
            return View();
        }

        public IActionResult Resources()
        {
            return View();
        }
        public IActionResult Bussiness_Showcase()
        {
            return View();
        }
    }
}

