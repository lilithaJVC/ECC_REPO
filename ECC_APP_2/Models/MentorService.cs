using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECC_APP_2.Models
{
    public class MentorService
    {
        private readonly HttpClient _httpClient;

        public MentorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LoginMentor(Mentor mentor)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Mentor/Login", mentor);
            return response.IsSuccessStatusCode;
        }
    }
}
