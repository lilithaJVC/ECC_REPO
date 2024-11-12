namespace ECC_APP_2.Models
{
    public class messagesViewModel
    {
        public List<Message> Messages { get; set; } = new List<Message>();
        public List<MessageResponses> Responses { get; set; } = new List<MessageResponses>(); // Keep this to store responses
        public int UnreadMessageCount { get; set; }
    }
}
