using System.Text.Json;
using System.Web;
using System.Collections.Generic;


namespace ForecastApi.ExternalServices.Weathers;

public class PointServices
{
    private const string endpoint = "points";
    private readonly HttpClient _httpClient;

    public PointServices(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WeatherService");;
    }

    public async Task<Point> GetPointAsync(PointRequest request)
    {
        string coordinates = $"{request.Latitude},{request.Longitude}";
        var response = await _httpClient.GetAsync($"{endpoint}/{coordinates}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Point>(content);
        return result ?? throw new Exception("Point failed");

    }
}

public class PointRequest
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
