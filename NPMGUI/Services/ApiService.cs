using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NPMGUI.DTOs;
using NPMGUI.Interfaces;

namespace NPMGUI.Services;

public class ApiService : IApiService
{
    private const string ApiUrl = "https://api.npms.io/v2/";
    
    private static HttpClient _client = new ()
    {
        BaseAddress = new Uri(ApiUrl)
    };
    
    public async Task<ApiPackageSearchResult?> SearchPackageAsync(string query)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"search?q={query}");
        
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine(jsonResponse);
        var searchResult = JsonConvert.DeserializeObject<ApiPackageSearchResult>(jsonResponse);
        return searchResult;
    }
}