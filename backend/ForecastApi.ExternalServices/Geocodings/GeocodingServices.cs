using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Options;

namespace ForecastApi.ExternalServices.Geocodings;

public class GeocodingServices
{
    private const string endpoint = "geocoder/geographies/address";
    private readonly HttpClient _httpClient;
    private readonly GeocodingConfiguration _configuration;
    public GeocodingServices(HttpClient httpClient, IOptions<GeocodingConfiguration> options)
    {
        _httpClient = httpClient;
        _configuration = options.Value;
    }

    public async Task<GeocodingResponse> GetGeocodingAsync(GeocodingRequest request)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["street"] = request.Street;
        query["city"] = request.City;
        query["state"] = request.State;
        query["zip"] = request.Zip;
        query["benchmark"] = _configuration.Benchmark;
        query["format"] = _configuration.Format;
        query["vintage"] = _configuration.Vintage;
        var uriBuilder = new UriBuilder(endpoint)
        {
            Query = query.ToString()
        };
        var response = await _httpClient.GetAsync(uriBuilder.Uri);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<GeocodingResponse>(content);
        return result ?? throw new Exception("Geocoding failed");
    }
}

public class GeocodingRequest
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
}

public class GeocodingResponse
{

}

public class GeocodingConfiguration
{
    public string Benchmark { get; set; }
    public string Format { get; set; }
    public string Vintage { get; set; }
}

