namespace ECC_APP_2.Models
{
    public class Message
    {
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
    }
}
