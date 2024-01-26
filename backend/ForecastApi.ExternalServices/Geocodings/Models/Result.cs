using System.Text.Json.Serialization;

namespace ForecastApi.ExternalServices.Geocodings.Models;

public class Result
{
    [JsonPropertyName("addressMatches")]
    public List<AddressMatch> AddressMatches { get; set; }
}
