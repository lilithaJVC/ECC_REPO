using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECC_APP_2.Models
{
    public class FundingGuideService
    {
        private readonly HttpClient _httpClient;

        public FundingGuideService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SaveFundingGuide(FundingGuideTemplate model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7187/api/FundingGuideTemplate", content);

            return response.IsSuccessStatusCode;
        }
    }
}
