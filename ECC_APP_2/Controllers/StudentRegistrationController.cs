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
