namespace ECC_APP_2.Models
{
    public class Mentor
    {
        //parameterless constructor for model binding
        public Mentor() { }
        public Mentor(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
