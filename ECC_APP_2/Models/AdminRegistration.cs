namespace ECC_APP_2.Models
{
    public class AdminRegistration
    {
        public AdminRegistration(string firstname, string lastname, string email, string password)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Password = password;
        }

        public String Firstname { get; set; }

        public String Lastname { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

    }
}
