using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers;

public class PointProperties
{
    [JsonPropertyName("gridId")]
    public string GridId { get; set; }

    [JsonPropertyName("gridX")]
    public int GridX { get; set; }

    [JsonPropertyName("gridY")]
    public int GridY { get; set; }
}
