using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers;

public class Point
{
    [JsonPropertyName("@context")]
    public List<object> Context { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; set; }

    [JsonPropertyName("properties")]
    public PointProperties Properties { get; set; }
}
