namespace ECC_APP_2.Models
{
    public class StudentRegistration
    {
        public StudentRegistration() { }

        public StudentRegistration(string firstname, string lastname, string email, string password)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Password = password;
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
