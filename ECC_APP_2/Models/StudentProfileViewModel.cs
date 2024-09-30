namespace ECC_APP_2.Models
{
    public class StudentProfileViewModel
    {
        public string UserEmail { get; set; }
        public string UserFirstname { get; set; }
        public int? UserID { get; set; } // Use nullable int to handle cases where the user ID might not be set
    }
}
