using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECC_APP_2.Models
{
    public class StudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterStudent(StudentRegistration student)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(student),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("api/Student/Register", content);

            return response.IsSuccessStatusCode;
        }
    }
}
