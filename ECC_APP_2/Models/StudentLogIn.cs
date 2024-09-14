namespace ECC_APP_2.Models
{
    public class StudentLogIn
    {
        public StudentLogIn() { } // Parameterless constructor

        public StudentLogIn(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
