using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers;

public class Forecast
{
    [JsonPropertyName("@context")]
    public List<object> Context { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; set; }

    [JsonPropertyName("properties")]
    public Properties Properties { get; set; }
}

