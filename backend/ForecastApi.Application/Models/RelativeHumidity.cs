using System.Text.Json.Serialization;

namespace ForecastApi.Application.Models;

public class RelativeHumidity
{
    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }
}

