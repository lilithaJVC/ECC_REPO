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
        private readonly FundingGuideService _fundingGuideService;
        private readonly AdminService _adminService;
        private readonly MentorService _mentorService; // Add MentorService
        private readonly ILogger<HomeController> _logger;

        // Static list to store feedback in-memory
        private static List<Feedback> _feedbackList = new List<Feedback>();

        public HomeController(StudentService studentService, BusinessProposalService businessProposalService,
                              FundingGuideService fundingGuideService, AdminService adminService,
                              MentorService mentorService, ILogger<HomeController> logger) // Initialize MentorService
        {
            _studentService = studentService;
            _businessProposalService = businessProposalService;
            _fundingGuideService = fundingGuideService;
            _adminService = adminService;
            _mentorService = mentorService; // Initialize MentorService
            _logger = logger;
        }


        // Admin registration page
        public IActionResult AdminRegistration()
        {
            return View();
        }

        // Admin login page
        public IActionResult AdminLogin()
        {
            return View();
        }

        // Admin login POST method
        [HttpPost]
        public async Task<IActionResult> AdminLogin(AdminRegistration model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Call API for admin login
            var isSuccess = await _adminService.LoginAdmin(model);
            if (isSuccess)
            {
                TempData["Message"] = "Login successful!";
                // Redirect to a dashboard or admin panel after successful login
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Login failed. Please check your email and password.";
            }

            return View(model);
        }

        //student registration 

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
                // Store the email and first name in session
                HttpContext.Session.SetString("UserEmail", model.Email);
                HttpContext.Session.SetString("UserFirstname", model.Firstname);
                HttpContext.Session.SetInt32("UserID", model.studentNum);

                ViewBag.Message = "Login successful!";
                // Redirect to the Resources action after successful login
                return RedirectToAction("Resources");
            }
            else
            {
                ViewBag.Message = "Login failed. Please check your email and password.";
                return View(model);
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsStudentLoggedIn");
            TempData["Message"] = "You have been logged out.";
            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
          

            return View();
        }

      

        public IActionResult Networking()
        {
            return View();
        }

        public IActionResult Resources()
        {
            // Retrieve the first name from session
            ViewBag.UserFirstname = HttpContext.Session.GetString("UserFirstname");
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

            var studentNum = HttpContext.Session.GetInt32("UserID");
            if (studentNum.HasValue)
            {
                model.studentNum = studentNum.Value;
            }
            else
            {
                ModelState.AddModelError("", "User not logged in.");
                return View(model);
            }

            var isSuccess = await _fundingGuideService.SaveFundingGuide(model);
            if (isSuccess)
            {
                ViewBag.Message = "Funding Guide saved successfully!";
            }
            else
            {
                _logger.LogError("Failed to save Funding Guide. API returned a failure response.");
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
            else
            {
                return $"Funding Guide Template \n   \nFunding Guide ID: {fundingGuide.FundingGuideId}\n" +
                  $"Funding Purpose: {fundingGuide.fundingPurpose}\n" +
                      $"Bussiness Overview: {fundingGuide.bussinessOverview}\n" +
                        $"Business name: {fundingGuide.bussinessName}\n" +
                          $"Mission: {fundingGuide.mission}\n" +
                            $"BussinessModel {fundingGuide.bussinessModel}\n" +
                              $"TotalFunding: {fundingGuide.totalFunding}\n" +
                                $"UseOfFunds: {fundingGuide.useOfFunds}\n" +
                                  $"Expenses: {fundingGuide.expenses}\n" +

                                    $"Profitability: {fundingGuide.profitability}\n" +
                                      $"Industry: {fundingGuide.industry}\n" +
                                        $"Competitors: {fundingGuide.competitors}\n" +
                                          $"MarketTrends: {fundingGuide.marketTrends}\n" +
                                            $"KeyMembersAndRoles: {fundingGuide.KeyMembersandRoles}\n" +
                                              $"KeyMilestones: {fundingGuide.keyMilestones}\n" +
                                                $"Timeline: {fundingGuide.timeline}\n" +
                                                  $"Risks: {fundingGuide.risks}\n" +
                                                    $"RiskPlan: {fundingGuide.riskPlan}\n" +

                                                     $"Summary: {fundingGuide.summary}\n" +
                                                      $"Name: {fundingGuide.name}\n" +
                                                       $"Email: {fundingGuide.email}\n" +
                                                        $"PhoneNumber: {fundingGuide.phoneNumber}\n" +

                  $"Amount Requested: {fundingGuide.amountRequested}";

            }

        }



        // Business Proposal code

        public IActionResult BusinessProposal()
        {
            // Render the BusinessProposal form view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BusinessProposal(BussinessProposalTemplate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var studentNum = HttpContext.Session.GetInt32("UserID");
            if (studentNum.HasValue)
            {
                model.studentNum = studentNum.Value; // Assign the studentNum
            }
            else
            {
                ModelState.AddModelError("", "User not logged in.");
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

        // Mentor login GET method
        public IActionResult MentorLogin()
        {
            return View();
        }

        // Mentor login POST method
        [HttpPost]
        public async Task<IActionResult> MentorLogin(Mentor model) // Correct type to Mentor
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isSuccess = await _mentorService.LoginMentor(model); // Pass the correct model
            if (isSuccess)
            {
                // Store the mentor's email in session
                HttpContext.Session.SetString("MentorEmail", model.Email); // Assuming model has an Email property

                TempData["Message"] = "Login successful!";
                return RedirectToAction("MentorDashboard");
            }
            else
            {
                TempData["Message"] = "Login failed. Please check your email and password.";
            }

            return View(model);
        }


        public async Task<IActionResult> Bussiness_Showcase()
        {
            return View();
        }


        public async Task<IActionResult> MentorDashboard()
        {
            // Fetch all business proposals and funding guides
            var businessProposals = await _businessProposalService.GetAllBusinessProposals();
            var fundingGuides = await _fundingGuideService.GetAllFundingGuides();

            // Create a MentorDashboardViewModel and populate it with the proposals and guides
            var viewModel = new MentorDashboardViewModel
            {
                BusinessProposals = businessProposals,
                FundingGuides = fundingGuides
            };

            // Pass the view model to the view
            return View(viewModel);
        }


        public IActionResult StudentProfile()
       {
         // Retrieve user details from the session
         var userEmail = HttpContext.Session.GetString("UserEmail");
         var userFirstname = HttpContext.Session.GetString("UserFirstname");
         var userId = HttpContext.Session.GetInt32("UserID");

         // Create a view model to pass to the view
        var model = new StudentProfileViewModel
        {
        UserEmail = userEmail,
        UserFirstname = userFirstname,
        UserID = userId
         };

           return View(model);
        }





        // SubmitFeedback Action
        [HttpPost]
        public IActionResult SubmitFeedback(string mentorComment, int studentNum, string mentorEmail)
        {
            if (string.IsNullOrWhiteSpace(mentorComment))
            {
                TempData["Error"] = "Feedback cannot be empty.";
                return RedirectToAction("MentorDashboard");
            }

            // Create a new Feedback object
            var feedback = new Feedback
            {
                FeedbackText = mentorComment,
                MentorEmail = mentorEmail,
                StudentNumber = studentNum,
                SubmittedAt = DateTime.Now
            };

            // Add to the static list
            _feedbackList.Add(feedback);

            TempData["Message"] = "Feedback submitted successfully!";
            return RedirectToAction("MentorDashboard");
        }

        // StudentFeedback Action
        // StudentFeedback Action
        // StudentFeedback Action
        public IActionResult StudentFeedback()
        {
            // Retrieve the logged-in student's ID from the session
            var studentId = HttpContext.Session.GetInt32("UserID");

            // Check if studentId is valid
            if (!studentId.HasValue)
            {
                // Handle case where student is not logged in
                TempData["Error"] = "You must be logged in to view feedback.";
                return RedirectToAction("StLogIn"); // Redirect to login page if not logged in
            }

            // Filter feedback list to include only feedback for the logged-in student
            var feedbackForStudent = _feedbackList
                .Where(f => f.StudentNumber == studentId.Value)
                .ToList();

            // Create the view model with the filtered feedback
            var feedbackModel = new StudentFeedbackViewModel
            {
                FeedbackList = feedbackForStudent
            };

            return View(feedbackModel);
        }

       


    }
}
