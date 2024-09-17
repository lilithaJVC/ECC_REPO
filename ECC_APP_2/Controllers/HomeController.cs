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
        private readonly BusinessProposalService _businessProposalService; // Corrected name
        private readonly ILogger<HomeController> _logger;

        public HomeController(StudentService studentService, BusinessProposalService businessProposalService, ILogger<HomeController> logger)
        {
            _studentService = studentService;
            _businessProposalService = businessProposalService; // Corrected name
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

        // Student login code

        public IActionResult StLogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StLogIn(StudentLogIn model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isSuccess = await _studentService.LoginStudent(model);
            if (isSuccess)
            {
                // Store the email in session
                HttpContext.Session.SetString("UserEmail", model.Email);

                ViewBag.Message = "Login successful!";
                // Redirect to the Index action after successful login
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Login failed. Please check your email and password.";
                return View(model);
            }
        }

        public IActionResult Index()
        {
            // Retrieve the email from session
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Funding Guide code

        public IActionResult FundingGuide()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FundingGuide(FundingGuideTemplate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var fundingGuideService = new FundingGuideService(new HttpClient());
            var isSuccess = await fundingGuideService.SaveFundingGuide(model);
            if (isSuccess)
            {
                ViewBag.Message = "Funding Guide saved successfully!";
            }
            else
            {
                ViewBag.Message = "Failed to save Funding Guide. Please try again.";
            }

            return View(model);
        }

        public IActionResult LoadPartialView(string partialViewName)
        {
            return PartialView(partialViewName);
        }

        // Business Proposal code

        public IActionResult BusinessProposal()
        {
            // Render the BusinessProposal form view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BusinessProposal(BussinessProposalTemplate model) // Corrected model name
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isSuccess = await _businessProposalService.SaveBusinessProposal(model);
            if (isSuccess)
            {
                ViewBag.Message = "Business proposal successfully captured.";
            }
            else
            {
                ViewBag.Message = "Failed to save Business Proposal. Please try again.";
            }

            return View(model);  // Pass the model back to the view in case of errors
        }
    }
}