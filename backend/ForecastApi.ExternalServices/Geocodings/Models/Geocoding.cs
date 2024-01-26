using System.Text.Json.Serialization;

namespace ForecastApi.ExternalServices.Geocodings.Models;

public class Geocoding
{
    [JsonPropertyName("result")]
    public Result Result { get; set; }
}
