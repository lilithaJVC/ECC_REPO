using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic; 
using ECC_APP_2.Models; 

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

        public async Task<bool> LoginStudent(StudentLogIn student)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(student),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("api/Student/Login", content); 

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Students>> GetAllStudents()
        {
            var response = await _httpClient.GetAsync("api/Student");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                // Log the JSON response for debugging
                Console.WriteLine($"API Response: {json}");

                return JsonSerializer.Deserialize<List<Students>>(json);
            }

            return null; // or throw an exception
        }



    }
}
