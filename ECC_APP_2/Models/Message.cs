namespace ECC_APP_2.Models
{
    public class Message
    {
        public int MessageId { get; set; } // This is required to uniquely identify each message
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
        public int? ParentMessageId { get; set; } // Link to the parent message for replies
        public List<MessageResponses> Responses { get; set; } = new List<MessageResponses>(); // For storing replies
    }

}
