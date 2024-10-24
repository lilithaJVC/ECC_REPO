using Microsoft.AspNetCore.Mvc;
using ECC_APP_2.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECC_APP_2.Controllers
{
    public class StudentRegistrationController : Controller
    {
        private readonly StudentService _studentService;
        private readonly ILogger<HomeController> _logger;

        public StudentRegistrationController(StudentService studentService, ILogger<HomeController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterStudent()
        {
            return View();
        }



    }
}
