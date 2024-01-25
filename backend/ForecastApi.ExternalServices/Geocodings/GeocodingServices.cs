using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    public async Task<RootObject> GetGeocodingAsync(GeocodingRequest request)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["street"] = request.Street;
        query["city"] = request.City;
        query["state"] = request.State;
        query["zip"] = request.Zip;
        query["benchmark"] = _configuration.Benchmark;
        query["format"] = _configuration.Format;
        query["vintage"] = _configuration.Vintage;
        var relativeUrl = $"{endpoint}?{query}";
        var response = await _httpClient.GetAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<RootObject>(content);
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


public class GeocodingConfiguration
{
    public string Benchmark { get; set; }
    public string Format { get; set; }
    public string Vintage { get; set; }
}




public class RootObject
{
    [JsonPropertyName("result")]
    public Result Result { get; set; }
}

public class Result
{
    [JsonPropertyName("addressMatches")]
    public List<AddressMatch> AddressMatches { get; set; }
}

public class AddressMatch
{
    [JsonPropertyName("coordinates")]
    public Coordinates Coordinates { get; set; }
}

public class Coordinates
{
    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }
}
