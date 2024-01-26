using System.Text.Json.Serialization;

namespace ForecastApi.Application.Models;

public class Dewpoint
{
    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; }

    [JsonPropertyName("value")]
    public double Value { get; set; }
}

