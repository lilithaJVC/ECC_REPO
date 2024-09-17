

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ECC_APP_2.Models;

public class BusinessProposalService
{
    private readonly HttpClient _httpClient;

    public BusinessProposalService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> SaveBusinessProposal(BussinessProposalTemplate model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/BusinessProposalTemplate", model);
        return response.IsSuccessStatusCode;
    }
}
