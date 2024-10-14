using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; // Add this

namespace ECC_APP_2.Models
{
    public class Students
    {
        [Key]
        public int StudentNum { get; set; }

        [JsonPropertyName("firstname")] // Map JSON property name to model property
        public string Firstname { get; set; }

        [JsonPropertyName("lastname")] // Map JSON property name to model property
        public string Lastname { get; set; }

        [JsonPropertyName("email")] // Map JSON property name to model property
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
