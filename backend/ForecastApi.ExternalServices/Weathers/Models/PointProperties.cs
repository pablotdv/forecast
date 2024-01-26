using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers.Models;

public class PointProperties
{
    [JsonPropertyName("gridId")]
    public string GridId { get; set; }

    [JsonPropertyName("gridX")]
    public int GridX { get; set; }

    [JsonPropertyName("gridY")]
    public int GridY { get; set; }
}
