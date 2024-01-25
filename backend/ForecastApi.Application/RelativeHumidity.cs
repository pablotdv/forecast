using System.Text.Json.Serialization;

namespace ForecastApi.Application;

public class RelativeHumidity
{
    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }
}

