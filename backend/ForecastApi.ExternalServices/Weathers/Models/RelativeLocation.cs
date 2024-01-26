using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers.Models;

public class RelativeLocation
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; set; }

    [JsonPropertyName("properties")]
    public RelativeLocationProperties Properties { get; set; }
}
