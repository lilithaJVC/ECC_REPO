using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ECC_APP_2.Models
{
    public class AdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to login admin via API
        public async Task<bool> LoginAdmin(AdminRegistration model)
        {
            // API call to check admin login credentials
            var response = await _httpClient.PostAsJsonAsync("api/AdminLogin", model);

            // For debugging or logging the API response
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);  // Log response content
            }

            return response.IsSuccessStatusCode;
        }
    }
}
