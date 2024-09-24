namespace ECC_APP_2.Models
{
    public class StudentLogIn
    {
        public StudentLogIn() { } // Parameterless constructor

        public StudentLogIn(string email, string password, string firstname, int studentNum)
        {
            Email = email;
            Password = password;
            Firstname = firstname;
            this.studentNum = studentNum;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public String Firstname { get; set; }

        public int studentNum { get; set; }




    }
}
