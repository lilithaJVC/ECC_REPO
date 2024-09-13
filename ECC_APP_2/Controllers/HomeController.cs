using ECC_APP_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ECC_APP_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentService _studentService;
        private readonly ILogger<HomeController> _logger;

        // Ensure there is only one constructor, with all necessary dependencies
        public HomeController(StudentService studentService, ILogger<HomeController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        public IActionResult RegisterStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(StudentRegistration model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isSuccess = await _studentService.RegisterStudent(model);
            if (isSuccess)
            {
                ViewBag.Message = "Registration successful!";
            }
            else
            {
                ViewBag.Message = "Registration failed. Please try again.";
            }

            return View(model);  // Pass the model back to the view in case of errors
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
    }
}
