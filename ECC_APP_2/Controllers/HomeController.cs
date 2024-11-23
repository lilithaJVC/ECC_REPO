using ECC_APP_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
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





        // Admin registration view

        //Code attribution 
        //stack overflow: https://stackoverflow.com/questions/44376801/mvc-application-use-your-local-db-in-order-to-login-register



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
                return RedirectToAction("AdminDash");
            }
            else
            {
                TempData["Message"] = "Login failed. Please check your email and password.";
            }

            return View(model);
        }







        //student registraion code 
        //Code attribution 
        //stack overflow: https://stackoverflow.com/questions/44376801/mvc-application-use-your-local-db-in-order-to-login-register


        public IActionResult AdminDash()
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
        //Code attribution 
        //stack overflow: https://stackoverflow.com/questions/44376801/mvc-application-use-your-local-db-in-order-to-login-register


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


      



        //resources code 

        public IActionResult Resources()
        {
            // Retrieve the first name from session
            ViewBag.UserFirstname = HttpContext.Session.GetString("UserFirstname");

            // Retrieve the logged-in student's ID from the session
            var studentId = HttpContext.Session.GetInt32("UserID");

            // Count new feedback
            int newFeedbackCount = 0;
            if (studentId.HasValue)
            {
                var feedbackForStudent = _feedbackList
                    .Where(f => f.StudentNumber == studentId.Value)
                    .ToList();

                // Assuming IsRead is a property to check if feedback has been read
                newFeedbackCount = feedbackForStudent.Count(f => !f.IsRead);
            }

            // Pass the new feedback count to the view
            ViewBag.NewFeedbackCount = newFeedbackCount;

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







        //Mentor code 
        //Code attribution 
        //stack overflow: https://stackoverflow.com/questions/44376801/mvc-application-use-your-local-db-in-order-to-login-register


        public IActionResult MentorLogin()
        {
            return View();
        }

        // Mentor login POST method
        //Code attribution 
        //stack overflow: https://stackoverflow.com/questions/44376801/mvc-application-use-your-local-db-in-order-to-login-register

        [HttpPost]
        public async Task<IActionResult> MentorLogin(Mentor model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isSuccess = await _mentorService.LoginMentor(model); 
            if (isSuccess)
            {
                // Store the mentor's email in session
                HttpContext.Session.SetString("MentorEmail", model.Email); 

                TempData["Message"] = "Login successful!";
                return RedirectToAction("MentorDashboard");
            }
            else
            {
                TempData["Message"] = "Login failed. Please check your email and password.";
            }

            return View(model);
        }







        //Bussiness Showcase code 
        public async Task<IActionResult> Bussiness_Showcase()
        {
            return View();
        }






        //Mentor Code
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




        //Student Profile code 
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





       //Prposal Feedback code 
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

        public IActionResult StudentFeedback()
        {
            var studentId = HttpContext.Session.GetInt32("UserID");

            if (!studentId.HasValue)
            {
                TempData["Error"] = "You must be logged in to view feedback.";
                return RedirectToAction("StLogIn");
            }

            var feedbackForStudent = _feedbackList.Where(f => f.StudentNumber == studentId.Value).ToList();
            var newFeedbackCount = feedbackForStudent.Count(f => !f.IsRead);

            foreach (var feedback in feedbackForStudent.Where(f => !f.IsRead))
            {
                feedback.IsRead = true;
            }

            var feedbackModel = new StudentFeedbackViewModel
            {
                FeedbackList = feedbackForStudent,
                NewFeedbackCount = newFeedbackCount
            };

            ViewBag.NewFeedbackCount = newFeedbackCount;

            return View(feedbackModel);
        }





        // Funding Guide code

        public IActionResult FundingGuide()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FundingGuide(FundingGuideTemplate model)
        {

            ViewBag.NewMessagesCount = await GetUnreadMessageCount();

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





        //networking code 
        //code attribution: 
        //Soclar winds: https://www.solarwinds.com/database?s_kwcid=AL!11508!3!708553666292!p!!g!!sql%20ssmscq_src=google_ads&cq_cmp=21563311874&cq_con=165671124453&cq_term=sql%20ssms&cq_med=&cq_plac=&cq_net=g&cq_plt=gp&&cq_plac=&cq_net=g&cq_pos=&cq_med=&cq_plt=gp&gad_source=1&gclid=CjwKCAiAl4a6BhBqEiwAqvrqupIQlgmhGZXSQGF5sk8EWDENGbERANcsXk3fFjCqVX8fOq6kek2w2hoCf0MQAvD_BwE&gclsrc=aw.ds
        public async Task<IActionResult> Networking()
        {
            // Retrieve the logged-in student's ID from the session
            var studentId = HttpContext.Session.GetInt32("UserID");

            // Check if studentId is valid
            if (!studentId.HasValue)
            {
                // Handle case where student is not logged in
                TempData["Error"] = "You must be logged in to view the networking page.";
                return RedirectToAction("StLogIn"); // Redirect to login page if not logged in
            }

            // Fetch students list
            var students = await _studentService.GetAllStudents();
            if (students == null)
            {
                // Handle the case where no students are found or the API call failed
                ViewBag.ErrorMessage = "Could not retrieve students.";
                return View(); // Return an empty view with an error message
            }

            // Process the students list using a loop
            foreach (var student in students)
            {

            }

            return View(students); // Pass the modified list of students to the view
        }


        //messages method 
        private static messagesViewModel messagesViewModel = new messagesViewModel();

        [HttpPost]
        public IActionResult SendMessage(string receiverEmail, string content, int? parentMessageId = null)
        {
            var senderEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(senderEmail))
            {
                return Json(new { success = false, message = "Sender email not found in session." });
            }

            // Create a new message object
            var newMessage = new Message
            {
                SenderEmail = senderEmail,
                ReceiverEmail = receiverEmail,
                Content = content,
                SentDate = DateTime.Now,
                IsRead = false,
                ParentMessageId = parentMessageId // Link to parent message if available
            };

            // Add the new message to the list
            messagesViewModel.Messages.Add(newMessage);

            // If the message has a parentMessageId, find the original message and link the reply
            if (parentMessageId.HasValue)
            {
                var originalMessage = messagesViewModel.Messages.FirstOrDefault(m => m.MessageId == parentMessageId);
                if (originalMessage != null)
                {
                    var response = new MessageResponses
                    {
                        ResponseContent = content,
                        ResponseDate = DateTime.Now,
                        SenderEmail = senderEmail
                    };

                    // Add the response to the original message
                    originalMessage.Responses.Add(response);
                }
            }

            // Add success message to TempData
            TempData["InviteMessage"] = "Message sent successfully!";

            // Calculate unread message count for the receiver
            var unreadCount = messagesViewModel.Messages.Count(m => m.ReceiverEmail == receiverEmail && !m.IsRead);

            return Json(new { success = true, unreadCount });
        }

        [HttpGet]
        public async Task< IActionResult> GetUnreadMessageCount()
        {
            var receiverEmail = HttpContext.Session.GetString("UserEmail");
            Console.WriteLine(receiverEmail);  // Debugging line

            // Calculate the unread message count based on receiver email and unread status
            var unreadCount = messagesViewModel.Messages
                .Count(m => m.ReceiverEmail == receiverEmail && !m.IsRead);

            return Json(new { unreadCount });
        }

        public async Task<IActionResult> StudentMessagesView()
        {
            // Await the result from GetUnreadMessageCount
            ViewBag.NewMessagesCount = await GetUnreadMessageCount();

            var receiverEmail = HttpContext.Session.GetString("UserEmail");

            // Filter messages by the receiver email and unread status
            var unreadMessages = messagesViewModel.Messages
                .Where(m => m.ReceiverEmail == receiverEmail && !m.IsRead).ToList();

            // Set the unread message count
            messagesViewModel.UnreadMessageCount = unreadMessages.Count();

            // Pass the message count to the view
            ViewBag.NewMessagesCount = messagesViewModel.UnreadMessageCount;

            return View(messagesViewModel);
        }


        [HttpPost]
        public IActionResult MarkMessagesAsRead()
        {
            var receiverEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(receiverEmail))
            {
                return Json(new { success = false, message = "Receiver email not found in session." });
            }

            // Mark all unread messages for this receiver as read
            var unreadMessages = messagesViewModel.Messages
                .Where(m => m.ReceiverEmail == receiverEmail && !m.IsRead);

            foreach (var message in unreadMessages)
            {
                message.IsRead = true;
            }

            return Json(new { success = true });
        }

    }
}


