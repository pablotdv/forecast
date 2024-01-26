using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers.Models;

public class Point
{
    [JsonPropertyName("properties")]
    public PointProperties Properties { get; set; }
}
