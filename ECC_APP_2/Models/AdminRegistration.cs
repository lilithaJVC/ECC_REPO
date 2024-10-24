namespace ECC_APP_2.Models
{
    public class AdminRegistration
    {
        // Parameterless constructor required for model binding
        public AdminRegistration()
        {
        }

        public AdminRegistration(string email, string password)
        {
            Email = email;
            Password = password;
        }

        // Constructor with parameters


        public string Email { get; set; }
        public string Password { get; set; }
    }
}
