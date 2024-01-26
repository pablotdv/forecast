using System.Text.Json.Serialization;

namespace ForecastApi.ExternalServices.Geocodings.Models;

public class Coordinates
{
    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }
}
