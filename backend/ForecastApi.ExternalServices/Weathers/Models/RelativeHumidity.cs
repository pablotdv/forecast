using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers.Models;

public class RelativeHumidity
{
    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }
}

