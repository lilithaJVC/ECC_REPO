﻿using System.Collections.Generic;

namespace ECC_APP_2.Models
{
    public class StudentFeedbackViewModel
    {
        public List<Feedback> FeedbackList { get; set; }
          public int NewFeedbackCount { get; set; } // Add this line
    }
}
