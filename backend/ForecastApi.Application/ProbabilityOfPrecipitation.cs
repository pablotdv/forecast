using System.Text.Json.Serialization;

namespace ForecastApi.Application;

public class ProbabilityOfPrecipitation
{
    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; }

    [JsonPropertyName("value")]
    public double? Value { get; set; }
}

