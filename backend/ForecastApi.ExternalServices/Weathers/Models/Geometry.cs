using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers.Models;

public class Geometry
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("coordinates")]
    public List<List<List<double>>> Coordinates { get; set; }
}

