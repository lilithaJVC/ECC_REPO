public class Feedback
{
    public string FeedbackText { get; set; }
    public string MentorEmail { get; set; }
    public int StudentNumber { get; set; }
    public DateTime SubmittedAt { get; set; }
    public bool IsRead { get; set; } // Ensure this property exists
}
