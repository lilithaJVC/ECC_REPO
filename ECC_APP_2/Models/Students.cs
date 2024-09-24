using System.ComponentModel.DataAnnotations;

namespace ECC_APP_2.Models
{
    public class Students
    {
        [Key]
        public int StudentNum { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
