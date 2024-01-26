using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers.Models;

public class Distance
{
    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; }

    [JsonPropertyName("value")]
    public double Value { get; set; }
}
