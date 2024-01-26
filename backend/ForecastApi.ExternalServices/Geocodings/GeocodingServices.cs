using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using ForecastApi.ExternalServices.Geocodings.Models;

namespace ForecastApi.ExternalServices.Geocodings;

public class GeocodingServices : IGeocodingServices
{
    private const string endpoint = "geocoder/geographies/address";
    private readonly HttpClient _httpClient;
    private readonly GeocodingConfiguration _configuration;
    public GeocodingServices(HttpClient httpClient, IOptions<GeocodingConfiguration> options)
    {
        _httpClient = httpClient;
        _configuration = options.Value;
    }

    public async Task<Geocoding> GetGeocodingAsync(GeocodingRequest request)
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
        var result = JsonSerializer.Deserialize<Geocoding>(content);
        return result ?? throw new Exception("Geocoding failed");
    }
}

public interface IGeocodingServices
{
    Task<Geocoding> GetGeocodingAsync(GeocodingRequest request);
}