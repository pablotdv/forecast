using System.Text.Json;
using System.Web;
using System.Collections.Generic;
using ForecastApi.ExternalServices.Weathers.Models;


namespace ForecastApi.ExternalServices.Weathers;

public class ForecastServices : IForecastServices
{    
    private readonly HttpClient _httpClient;

    public ForecastServices(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WeatherService");;
    }

    public async Task<Forecast> GetForecastAsync(ForecastRequest request)
    {
        string coordinates = $"{request.GridX},{request.GridY}";
        var response = await _httpClient.GetAsync($"gridpoints/{request.GridId}/{coordinates}/forecast");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Forecast>(content);
        return result ?? throw new Exception("Forecast failed");

    }
}

public interface IForecastServices
{
    Task<Forecast> GetForecastAsync(ForecastRequest request);
}

public class ForecastRequest
{
    public string GridId {get;set;}
    public int GridX { get; set; }
    public int GridY { get; set; }
}

