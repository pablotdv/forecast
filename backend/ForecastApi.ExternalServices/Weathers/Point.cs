using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers;

public class Point
{
    [JsonPropertyName("properties")]
    public PointProperties Properties { get; set; }
}
