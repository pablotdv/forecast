using System.Text.Json.Serialization;

namespace ForecastApi.Application;

public class Dewpoint
{
    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; }

    [JsonPropertyName("value")]
    public double Value { get; set; }
}

