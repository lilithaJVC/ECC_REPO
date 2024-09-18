using ECC_APP_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ECC_APP_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentService _studentService;
        private readonly BusinessProposalService _businessProposalService;
        private readonly FundingGuideService _fundingGuideService; // Added funding guide service
        private readonly ILogger<HomeController> _logger;

        public HomeController(StudentService studentService, BusinessProposalService businessProposalService, FundingGuideService fundingGuideService, ILogger<HomeController> logger)
        {
            _studentService = studentService;
            _businessProposalService = businessProposalService;
            _fundingGuideService = fundingGuideService; // Initialize funding guide service
            _logger = logger;
        }

        //administator login 

        public IActionResult AdminRegistration()
        {
            return View();
        }

        public IActionResult AdminLogin()
        {
            return View();
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
                return RedirectToAction("Resources");
            }
            else
            {
                ViewBag.Message = "Login failed. Please check your email and password.";
                return View(model);
            }
        }

        public IActionResult Index()
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
            // Retrieve the email from session
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
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

        public IActionResult DownloadTemplate()
        {
            // Define the ID for the funding guide you want to fetch
            int fundingGuideId = 1; // Replace with the actual ID or logic to get the ID
            var fileName = "BusinessProposalTemplate.docx";

            // Fetch the content asynchronously
            var fileContentTask = GenerateTemplateContent(fundingGuideId);
            fileContentTask.Wait(); // Wait for the task to complete synchronously
            var fileContent = fileContentTask.Result;

            // Create a file stream for the content
            var fileStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));

            // Return the file as a download
            return File(fileStream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        // Helper method to generate or fetch the template content
        private async Task<string> GenerateTemplateContent(int id)
        {
            var fundingGuide = await _fundingGuideService.GetFundingGuideById(id);

            if (fundingGuide == null)
            {
                return "Funding guide not found.";
            }

         
            return $"Funding Guide Template \n   \nFunding Guide ID: {fundingGuide.FundingGuideId}\n" +
                   $"Funding Purpose: {fundingGuide.FundingPurpose}\n" +
                       $"Bussiness Overview: {fundingGuide.BussinessOverview}\n" +
                         $"Business name: {fundingGuide.BussinessName}\n" +
                           $"Mission: {fundingGuide.Mission}\n" +
                             $"BussinessModel {fundingGuide.BussinessModel}\n" +
                               $"TotalFunding: {fundingGuide.TotalFunding}\n" +
                                 $"UseOfFunds: {fundingGuide.UseOfFunds}\n" +
                                   $"Expenses: {fundingGuide.expenses}\n" +

                                     $"Profitability: {fundingGuide.Profitability}\n" +
                                       $"Industry: {fundingGuide.industry}\n" +
                                         $"Competitors: {fundingGuide.Competitors}\n" +
                                           $"MarketTrends: {fundingGuide.marketTrends}\n" +
                                             $"KeyMembersAndRoles: {fundingGuide.keyMembersandRoles}\n" +
                                               $"KeyMilestones: {fundingGuide.keyMilestones}\n" +
                                                 $"Timeline: {fundingGuide.Timeline}\n" +
                                                   $"Risks: {fundingGuide.Risks}\n" +
                                                     $"RiskPlan: {fundingGuide.RiskPlan}\n" +

                                                      $"Summary: {fundingGuide.summary}\n" +
                                                       $"Name: {fundingGuide.name}\n" +
                                                        $"Email: {fundingGuide.email}\n" +
                                                         $"PhoneNumber: {fundingGuide.phoneNumber}\n" +
                                                        
                   $"Amount Requested: {fundingGuide.AmountRequested}";
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
